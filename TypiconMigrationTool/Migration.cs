using ScheduleHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using System.Data.Entity.Validation;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Easter;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Implementations;
using System.IO;

namespace TypiconMigrationTool
{
    public class Migration
    {
        IUnitOfWork _unitOfWork;
        ScheduleHandler _sh;

        public Migration (IUnitOfWork unitOfWork, ScheduleHandler sh)
        {
            _unitOfWork = unitOfWork;
            _sh = sh;
        }

        public void Execute()
        {
            //Console.WriteLine("ClearEF()");
            //ClearEF();

            Console.WriteLine("Migrate()");
            Migrate();

            Console.WriteLine("MigrateEasters()");
            MigrateEasters();

            Console.WriteLine("Saving...");

            try
            {
                _unitOfWork.Commit();
                Console.WriteLine("Success.");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Object: " + validationError.Entry.Entity.ToString());
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        Console.WriteLine(err.ErrorMessage);
                    }
                }
            }
        }

        

        private void ClearEF()
        {
            IEnumerable<RuleEntity> rulesDaySet = _unitOfWork.Repository<RuleEntity>().GetAll();
            foreach (RuleEntity element in rulesDaySet)
            {
                _unitOfWork.Repository<RuleEntity>().Delete(element);
            }

            IEnumerable<Sign> dSet = _unitOfWork.Repository<Sign>().GetAll();
            foreach (Sign element in dSet)
            {
                _unitOfWork.Repository<Sign>().Delete(element);
            }

            IEnumerable<FolderEntity> foldersDaySet = _unitOfWork.Repository<FolderEntity>().GetAll();
            foreach (FolderEntity element in foldersDaySet)
            {
                _unitOfWork.Repository<FolderEntity>().Delete(element);
            }

            IEnumerable<TypiconEntity> typiconEntitySet = _unitOfWork.Repository<TypiconEntity>().GetAll();
            foreach (TypiconEntity element in typiconEntitySet)
            {
                _unitOfWork.Repository<TypiconEntity>().Delete(element);
            }

            //IEnumerable<MenologyDay> menologyDaySet = _unitOfWork.Repository<MenologyDay>().GetAll();
            //foreach (MenologyDay element in menologyDaySet)
            //{
            //    _unitOfWork.Repository<MenologyDay>().Delete(element);
            //}

            //IEnumerable<TriodionDay> triodionDaySet = _unitOfWork.Repository<TriodionDay>().GetAll();
            //foreach (TriodionDay element in triodionDaySet)
            //{
            //    _unitOfWork.Repository<TriodionDay>().Delete(element);
            //}

            _unitOfWork.Commit();
        }

        private void Migrate()
        {
            TypiconEntity typiconEntity = new TypiconEntity()
            {
                Name = "Типикон",
                Settings = new TypiconSettings()
                {
                    DefaultLanguage = "cs-ru",
                }
            };

            typiconEntity.RulesFolder = new TypiconFolderEntity() { Name = "Правила", Owner = typiconEntity };

            string folderPath = Properties.Settings.Default.FolderPath + typiconEntity.Name + "\\Sign\\"; 

            FileReader fileReader = new FileReader(folderPath);

            int i = 1;

            foreach (ScheduleDBDataSet.ServiceSignsRow row in _sh.DataSet.ServiceSigns.Rows)
            {
                //int priority = _signPriorites.ContainsKey(row.Name) ? _signPriorites[row.Name] : row.ID;

                SignMigrator signMigrator = new SignMigrator(row.ID);

                Sign sign = new Sign()
                {
                    //Id = signMigrator.NewId,
                    Name = row.Name,
                    Number = signMigrator.NewId,
                    Priority = signMigrator.Priority,
                    Owner = typiconEntity,
                    RuleDefinition = fileReader.GetXml(row.Name),
                    SignName = new ItemText()
                };

                sign.SignName.AddElement("cs-ru", row.Name);

                if (signMigrator.TemplateId != null)
                {
                    sign.Template = typiconEntity.Signs.First(c => c.Number == signMigrator.TemplateId);
                }

                //_unitOfWork.Repository<Sign>().Insert(sign);
                typiconEntity.Signs.Add(sign);

                i++;
            }

            _unitOfWork.Repository<TypiconEntity>().Insert(typiconEntity);

            try
            {
                _unitOfWork.Commit();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult result in ex.EntityValidationErrors)
                {
                    Console.WriteLine(result.ToString());
                }
            }
            MigrateMenologyDaysAndRules(typiconEntity);
            MigrateTriodionDaysAndRules(typiconEntity);
            MigrateCommonRules(typiconEntity);
        }

        private void MigrateMenologyDaysAndRules(TypiconEntity typiconEntity)
        {
            Console.WriteLine("MigrateMenologyDaysAndRules()");

            Timer timer = new Timer();
            timer.Start();

            //TypiconFolderEntity folder = new TypiconFolderEntity() { Name = "Минея" };
            //typiconEntity.RulesFolder.AddFolder(folder);

            //TypiconFolderEntity childFolder = new TypiconFolderEntity() { Name = "Минея 1" };

            //folder.AddFolder(childFolder);

            string folderRulePath = Properties.Settings.Default.FolderPath + typiconEntity.Name + "\\Menology\\";

            FileReader fileRuleReader = new FileReader(folderRulePath);

            string folderDayPath = Properties.Settings.Default.FolderPath + "Menology\\";

            FileReader fileDayReader = new FileReader(folderDayPath);

            foreach (ScheduleDBDataSet.MineinikRow mineinikRow in _sh.DataSet.Mineinik.Rows)
            {
                DayService dayService = new DayService();

                //наполняем содержимое текста службы
                dayService.ServiceName.AddElement("cs-ru", mineinikRow.Name);
                dayService.IsCelebrating = mineinikRow.Rule == "1";

                ItemDate d = (!mineinikRow.IsDateBNull()) ? new ItemDate(mineinikRow.DateB.Month, mineinikRow.DateB.Day) : null;

                string fileName = (d != null) ? d.Expression + "." + mineinikRow.Name : mineinikRow.Name;
                dayService.DayDefinition = fileDayReader.GetXml(fileName);

                //menologyDay
                //смотрим, есть ли уже такой объект с заявленной датой
                MenologyDay menologyDay = null;

                if (!mineinikRow.IsDateBNull())
                {
                    menologyDay = _unitOfWork.Repository<MenologyDay>()
                    .Get(c => c.DateB.Expression.Equals(d.Expression));
                }

                //если нет - создаем
                if (menologyDay == null)
                {
                    menologyDay = new MenologyDay()
                    {
                        //Name = mineinikRow.Name,
                        //DayName = XmlHelper.CreateItemTextCollection(
                        //    new CreateItemTextRequest() { Text = mineinikRow.Name, Name = "Name" }),
                        Date = (mineinikRow.IsDateNull()) ? new ItemDate() : new ItemDate(mineinikRow.Date.Month, mineinikRow.Date.Day),
                        DateB = (mineinikRow.IsDateBNull()) ? new ItemDate() : new ItemDate(mineinikRow.DateB.Month, mineinikRow.DateB.Day),
                    };

                    _unitOfWork.Repository<MenologyDay>().Insert(menologyDay);
                }

                menologyDay.AppendDayService(dayService);

                //menologyRule
                //смотрим, есть ли уже такой объект с заявленной датой
                MenologyRule menologyRule = null;

                if (!mineinikRow.IsDateBNull())
                {
                    
                    menologyRule = _unitOfWork.Repository<MenologyRule>()
                    .Get(c => c.DateB.Expression.Equals(d.Expression));
                }

                //если нет - создаем
                if (menologyRule == null)
                {
                    menologyRule = new MenologyRule()
                    {
                        //Name = menologyDay.Name,
                        Date = menologyDay.Date,
                        DateB = menologyDay.DateB,
                        Owner = typiconEntity,
                        Template = typiconEntity.Signs.First(c => c.Number == SignMigrator.Instance(mineinikRow.SignID).NewId),
                    };

                    menologyRule.DayServices.Add(dayService);

                    typiconEntity.MenologyRules.Add(menologyRule);
                    //_unitOfWork.Repository<MenologyRule>().Insert(menologyRule);

                    //берем xml-правило из файла
                    menologyRule.RuleDefinition = (!mineinikRow.IsDateBNull()) 
                                                    ? fileRuleReader.GetXml(menologyDay.DateB.Expression)
                                                    : fileRuleReader.GetXml(menologyRule.Name);
                }
                else
                {
                    menologyRule.DayServices.Add(dayService);
                }

                _unitOfWork.Commit();

                

                //DayService dayService = new DayService();

                //dayService.ServiceName.AddElement("cs-ru", mineinikRow.Name);

                //MenologyDay menologyDay = new MenologyDay()
                //{
                //    //Name = mineinikRow.Name,
                //    //DayName = XmlHelper.CreateItemTextCollection(
                //    //    new CreateItemTextRequest() { Text = mineinikRow.Name, Name = "Name" }),
                //    Date = (mineinikRow.IsDateNull()) ? new ItemDate() : new ItemDate(mineinikRow.Date.Month, mineinikRow.Date.Day),
                //    DateB = (mineinikRow.IsDateBNull()) ? new ItemDate() : new ItemDate(mineinikRow.DateB.Month, mineinikRow.DateB.Day),
                //};
                //menologyDay.AppendDayService(dayService);

                //_unitOfWork.Repository<MenologyDay>().Insert(menologyDay);

                //string ruleDefinition = "";



                //MenologyRule menologyRule = new MenologyRule()
                //{
                //    //Name = menologyDay.Name,
                //    Date = menologyDay.Date,
                //    DateB = menologyDay.DateB,
                //    Owner = typiconEntity,
                //    Template = typiconEntity.Signs.First(c => c.Number == SignMigrator.Instance(mineinikRow.SignID).NewId),
                //};
                //menologyRule.DayServices = new List<DayService>() { dayService };

                //if (!mineinikRow.IsDateBNull())
                //{
                //    //TODO: изменил алгоритм. Надо поменять все названия xml-файлов правил для Минеи
                //    string fileName = menologyDay.DateB.Expression;// + "." + menologyRule.Name;
                //    ruleDefinition = fileReader.GetXml(fileName);
                //}
                //else
                //{
                //    ruleDefinition = fileReader.GetXml(menologyRule.Name);
                //}

                //menologyRule.RuleDefinition = ruleDefinition;

                ////folder.AddRule(menologyRule);
                //typiconEntity.MenologyRules.Add(menologyRule);
                //_unitOfWork.Repository<MenologyRule>().Insert(menologyRule);
            }

            timer.Stop();
            Console.WriteLine(timer.GetStringValue());

            //_unitOfWork.Commit();
        }

        private void MigrateTriodionDaysAndRules(TypiconEntity typiconEntity)
        {
            Console.WriteLine("MigrateTriodionDaysAndRules()");

            Timer timer = new Timer();
            timer.Start();

            TypiconFolderEntity folder = new TypiconFolderEntity() { Name = "Триодь" };

            typiconEntity.RulesFolder.AddFolder(folder);

            foreach (ScheduleDBDataSet.TriodionRow row in _sh.DataSet.Triodion.Rows)
            {
                //CreateItemTextRequest req = new CreateItemTextRequest();
                //req.Text = row.Name;
                //req.Name = "Name1";
                //req.Style.IsBold = row.IsNameBold;
                //ItemTextCollection itemTextCol = XmlHelper.CreateItemTextCollection(req);

                DayService dayService = new DayService()
                {
                    ServiceName = new ItemText()
                };
                dayService.ServiceName.Style.IsBold = row.IsNameBold;
                dayService.ServiceName.AddElement("cs-ru", row.Name);

                TriodionDay day = new TriodionDay()
                {
                    //Name = row.Name,
                    //DayName = itemTextCol,
                    DaysFromEaster = (int) row.DayFromEaster,
                };

                day.AppendDayService(dayService);
                //day.Sign = _unitOfWork.Repository<Sign>().Get(c => c.Id == row.SignID);

                _unitOfWork.Repository<TriodionDay>().Insert(day);

                string folderPath = Properties.Settings.Default.FolderPath + typiconEntity.Name + "\\Triodion\\";

                FileReader fileReader = new FileReader(folderPath);

                TriodionRule rule = new TriodionRule()
                {
                    //Name = day.Name,
                    DaysFromEaster = day.DaysFromEaster,
                    Owner = typiconEntity,
                    Template = typiconEntity.Signs.First(c => c.Number == SignMigrator.Instance(row.SignID).NewId),
                    RuleDefinition = fileReader.GetXml(row.DayFromEaster.ToString())
                };
                rule.DayServices = new List<DayService>() { dayService };

                folder.AddRule(rule);
                typiconEntity.TriodionRules.Add(rule);
            }

            timer.Stop();
            Console.WriteLine(timer.GetStringValue());

            //_unitOfWork.Commit();
        }

        private void MigrateCommonRules(TypiconEntity typiconEntity)
        {
            Console.WriteLine("MigrateCommonRules()");

            Timer timer = new Timer();
            timer.Start();

            string folderPath = Properties.Settings.Default.FolderPath + typiconEntity.Name + "\\Common\\";

            FileReader fileReader = new FileReader(folderPath);

            IEnumerable<FilesSearchResponse> files = fileReader.GetXmlsFromDirectory();

            foreach (FilesSearchResponse file in files)
            {
                CommonRule commonRule = new CommonRule()
                {
                    Name = file.Name,
                    RuleDefinition = file.Xml,
                    Owner = typiconEntity
                };
                typiconEntity.CommonRules.Add(commonRule);
            }

            timer.Stop();
            Console.WriteLine(timer.GetStringValue());
        }

        private void MigrateEasters()
        {            
            foreach (ScheduleDBDataSet.PaskhaliaRow row in _sh.DataSet.Paskhalia)
            {
                _unitOfWork.Repository<EasterItem>().Insert(new EasterItem { Date = row.Date });
            }
            
            //_unitOfWork.Repository<EasterStorage>().Insert(EasterStorage.Instance);
        }
    }
}
