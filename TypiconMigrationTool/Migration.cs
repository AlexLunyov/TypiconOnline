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
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Implementations;
using System.IO;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Migration;
using TypiconOnline.AppServices.Implementations.Books;

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

            MigrateTheotokionIrmologion();

            Commit();
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
                    IsExceptionThrownWhenInvalid = true
                }
            };

            //typiconEntity.RulesFolder = new TypiconFolderEntity() { Name = "Правила", Owner = typiconEntity };

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
                    RuleDefinition = fileReader.Read(row.Name),
                    SignName = new ItemText()
                };

                sign.SignName.AddElement("cs-ru", row.Name);

                if (signMigrator.TemplateId != null)
                {
                    sign.Template = typiconEntity.Signs.First(c => c.Number == signMigrator.TemplateId);
                }

                typiconEntity.Signs.Add(sign);

                i++;
            }

            _unitOfWork.Repository<TypiconEntity>().Insert(typiconEntity);

            Commit();

            MigrateMenologyDaysAndRules(typiconEntity);
            MigrateTriodionDaysAndRules(typiconEntity);
            MigrateCommonRules(typiconEntity);

            Commit();
        }

        private void MigrateTheotokionIrmologion()
        {
            string folder = Path.Combine(Properties.Settings.Default.FolderPath, @"Books\Irmologion\Theotokion");

            IrmologionTheotokionFileReader reader = new IrmologionTheotokionFileReader(new FileReader(folder));

            IrmologionTheotokionService service = new IrmologionTheotokionService(_unitOfWork);

            IrmologionTheotokionFactory factory = new IrmologionTheotokionFactory();

            IMigrationManager manager = new IrmologionTheotokionMigrationManager(factory, reader, service);

            manager.Migrate();
        }

        private void Commit()
        {
            try
            {
                Console.WriteLine("Saving...");

                Timer timer = new Timer();
                timer.Start();

                _unitOfWork.Commit();

                Console.WriteLine("Success.");
                timer.Stop();
                Console.WriteLine(timer.GetStringValue());
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
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

            MenologyDay menologyDay = null;
            MenologyRule menologyRule = null;

            MigrationDayServiceFactory factory = new MigrationDayServiceFactory(Properties.Settings.Default.FolderPath);

            foreach (ScheduleDBDataSet.MineinikRow mineinikRow in _sh.DataSet.Mineinik.Rows)
            {
                factory.Initialize(mineinikRow);

                DayService dayService = factory.Create();

                ItemDate d = (!mineinikRow.IsDateBNull()) ? new ItemDate(mineinikRow.DateB.Month, mineinikRow.DateB.Day) : new ItemDate();

                //menologyDay
                /* Чтобы лишний раз не обращаться к БД,
                 * смотрим, не один и тот же MenologyDay, что и предыдущая строка из Access
                */
                if (menologyDay == null || !menologyDay.DateB.Expression.Equals(d.Expression))
                {
                    //нет - создаем новый день
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
                /*смотрим, есть ли уже такой объект с заявленной датой
                 * если дата пустая - т.е. праздник переходящий - значит 
                */
                if (menologyRule == null || d.IsEmpty || (!d.IsEmpty && !menologyRule.DateB.Expression.Equals(d.Expression)))
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

                    //берем xml-правило из файла
                    menologyRule.RuleDefinition = (!mineinikRow.IsDateBNull())
                                                    ? fileRuleReader.Read(menologyDay.DateB.Expression)
                                                    : fileRuleReader.Read(menologyRule.Name);
                }
                else
                {
                    menologyRule.DayServices.Add(dayService);
                }
            }

            timer.Stop();
            Console.WriteLine(timer.GetStringValue());
        }

        private void MigrateTriodionDaysAndRules(TypiconEntity typiconEntity)
        {
            Console.WriteLine("MigrateTriodionDaysAndRules()");

            Timer timer = new Timer();
            timer.Start();

            //TypiconFolderEntity folder = new TypiconFolderEntity() { Name = "Триодь" };

            //typiconEntity.RulesFolder.AddFolder(folder);

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
                    RuleDefinition = fileReader.Read(row.DayFromEaster.ToString())
                };
                rule.DayServices = new List<DayService>() { dayService };

                //folder.AddRule(rule);
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

            IEnumerable<FilesSearchResponse> files = fileReader.ReadsFromDirectory();

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
