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
using TypiconOnline.AppServices.Migration.Psalter;
using TypiconOnline.Domain.Books.Psalter;

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
            Console.WriteLine("MigrateTheotokionIrmologion()");
            MigrateTheotokionIrmologion();
            Console.WriteLine("MigrateOktoikh()");
            MigrateOktoikh();
            Console.WriteLine("MigrateKatavasia()");
            MigrateKatavasia();

            Commit();
        }               

        private void ClearEF()
        {
            IEnumerable<TypiconEntity> typiconEntitySet = _unitOfWork.Repository<TypiconEntity>().GetAll();
            foreach (TypiconEntity element in typiconEntitySet)
            {
                _unitOfWork.Repository<TypiconEntity>().Remove(element);
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

            _unitOfWork.SaveChanges();
        }

        private void Migrate()
        {
            TypiconEntity typiconEntity = new TypiconEntity()
            {
                Name = "Типикон",
                Settings = new TypiconSettings()
                {
                    DefaultLanguage = "cs-ru",
                    //IsExceptionThrownWhenInvalid = true
                }
            };

            //typiconEntity.RulesFolder = new TypiconFolderEntity() { Name = "Правила", Owner = typiconEntity };

            string folderPath = Path.Combine(Properties.Settings.Default.FolderPath, typiconEntity.Name, "Sign"); 

            FileReader fileReader = new FileReader(folderPath);

            int i = 1;

            foreach (ScheduleDBDataSet.ServiceSignsRow row in _sh.DataSet.ServiceSigns.Rows)
            {
                //int priority = _signPriorites.ContainsKey(row.Name) ? _signPriorites[row.Name] : row.ID;

                SignMigrator signMigrator = new SignMigrator(row.Number);

                Sign sign = new Sign()
                {
                    //Id = signMigrator.NewId,
                    Name = row.Name,
                    Priority = signMigrator.Priority,
                    Owner = typiconEntity,
                    IsTemplate = row.IsTemplate,
                    RuleDefinition = fileReader.Read(row.Name),
                    //SignName = new ItemText()// { StringExpression = row.Name }
                };

                if (signMigrator.Number != null)
                {
                    sign.Number = (int)signMigrator.Number;
                }

                sign.SignName.AddOrUpdate("cs-ru", row.Name);

                if (signMigrator.TemplateId != null)
                {
                    sign.Template = typiconEntity.Signs.First(c => c.Number == signMigrator.TemplateId);
                    sign.IsAddition = true;
                }

                typiconEntity.Signs.Add(sign);

                i++;
            }

            _unitOfWork.Repository<TypiconEntity>().Add(typiconEntity);

            Commit();

            MigrateMenologyDaysAndRules(typiconEntity);
            MigrateTriodionDaysAndRules(typiconEntity);
            MigrateCommonRules(typiconEntity);

            Commit();

            MigratePsalms();
            MigrateKathismas(typiconEntity);            
        }

        private void MigrateTheotokionIrmologion()
        {
            string folder = Path.Combine(Properties.Settings.Default.FolderPath, @"Books\Irmologion\Theotokion");

            ITheotokionAppFileReader reader = new TheotokionAppFileReader(new FileReader(folder));

            ITheotokionAppService service = new TheotokionAppService(_unitOfWork);

            ITheotokionAppFactory factory = new TheotokionAppFactory();

            IMigrationManager manager = new TheotokionAppMigrationManager(factory, reader, service);

            manager.Migrate();
        }

        private void MigrateOktoikh()
        {
            string folder = Path.Combine(Properties.Settings.Default.FolderPath, @"Books\Oktoikh");

            IOktoikhDayFileReader reader = new OktoikhDayFileReader(new FileReader(folder));

            IEasterContext easterContext = new EasterContext(_unitOfWork);

            IOktoikhDayService service = new OktoikhDayService(_unitOfWork, easterContext);

            IOktoikhDayFactory factory = new OktoikhDayFactory();

            IMigrationManager manager = new OktoikhDayMigrationManager(factory, reader, service);

            manager.Migrate();
        }

        private void MigrateKatavasia()
        {
            string folder = Path.Combine(Properties.Settings.Default.FolderPath, @"Books\Katavasia");

            IFileReader reader = new FileReader(folder);

            IKatavasiaService service = new KatavasiaService(_unitOfWork);

            IKatavasiaFactory factory = new KatavasiaFactory();

            IMigrationManager manager = new KatavasiaMigrationManager(factory, reader, service);

            manager.Migrate();
        }

        private void MigratePsalms()
        {
            Console.WriteLine("MigratePsalms()");
            string folder = Path.Combine(Properties.Settings.Default.FolderPath, @"Books\Psalter");

            var service = new PsalterService(_unitOfWork);

            var manager = new PsalmsMigrationManager(service);

            manager.MigratePsalms(new PsalterRuReader(folder, "cs-ru"));
            Commit();
            manager.MigratePsalms(new PsalterCsReader(folder, "cs-cs"));
            Commit();
        }

        private void MigrateKathismas(TypiconEntity typiconEntity)
        {
            Console.WriteLine("MigrateKathismas(TypiconEntity typiconEntity)");
            string folder = Path.Combine(Properties.Settings.Default.FolderPath, @"Books\Psalter");

            var context = new PsalterContext(_unitOfWork);

            var manager = new KathismasMigrationManager(context);
            manager.MigrateKathismas(new PsalterRuReader(folder, "cs-ru"), typiconEntity);
            //Commit();
            manager.MigrateKathismas(new PsalterCsReader(folder, "cs-cs"), typiconEntity, true);
            Commit();
        }

        private void Commit()
        {
            try
            {
                Console.WriteLine("Saving...");

                Timer timer = new Timer();
                timer.Start();

                _unitOfWork.SaveChanges();

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

            string folderRulePath = Path.Combine(Properties.Settings.Default.FolderPath, typiconEntity.Name, "Menology");

            FileReader fileRuleReader = new FileReader(folderRulePath);

            MenologyDay menologyDay = null;
            MenologyRule menologyRule = null;

            MigrationDayWorshipFactory factory = new MigrationDayWorshipFactory(Properties.Settings.Default.FolderPath);

            foreach (ScheduleDBDataSet.MineinikRow mineinikRow in _sh.DataSet.Mineinik.Rows)
            {
                factory.Initialize(mineinikRow);

                DayWorship dayWorship = factory.Create();

                ItemDate d = (!mineinikRow.IsDateBNull()) ? new ItemDate(mineinikRow.DateB.Month, mineinikRow.DateB.Day) : new ItemDate();

                //menologyDay
                /* Чтобы лишний раз не обращаться к БД,
                 * смотрим, не один и тот же MenologyDay, что и предыдущая строка из Access
                */
                //if (menologyDay == null || !menologyDay.DateB.Expression.Equals(d.Expression))
                menologyDay = _unitOfWork.Repository<MenologyDay>().Get(c => c.DateB.Expression.Equals(d.Expression));
                if (menologyDay == null)
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

                    _unitOfWork.Repository<MenologyDay>().Add(menologyDay);
                }
                

                menologyDay.AppendDayService(dayWorship);

                //menologyRule
                /*смотрим, есть ли уже такой объект с заявленной датой
                 * если дата пустая - т.е. праздник переходящий - значит 
                */

                if (!d.IsEmpty)
                {
                    menologyRule = typiconEntity.GetMenologyRule(mineinikRow.DateB);
                }

                if (menologyRule == null || d.IsEmpty)
                {
                    menologyRule = new MenologyRule()
                    {
                        //Name = menologyDay.Name,
                        Date = menologyDay.Date,
                        DateB = menologyDay.DateB,
                        Owner = typiconEntity,
                        //IsAddition = true,
                        Template = typiconEntity.Signs.First(c => c.SignName.FirstOrDefault("cs-ru").Text == mineinikRow.ServiceSignsRow.Name),
                    };

                    menologyRule.DayRuleWorships.Add( new DayRuleWorship() { DayRule = menologyRule, DayWorship = dayWorship } );

                    typiconEntity.MenologyRules.Add(menologyRule);

                    //берем xml-правило из файла
                    menologyRule.RuleDefinition = (!mineinikRow.IsDateBNull())
                                                    ? fileRuleReader.Read(menologyDay.DateB.Expression)
                                                    : fileRuleReader.Read(menologyRule.Name);
                }
                else
                {
                    menologyRule.DayRuleWorships.Add(new DayRuleWorship() { DayRule = menologyRule, DayWorship = dayWorship });
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

                DayWorship dayWorship = new DayWorship()
                {
                    WorshipName = new ItemTextStyled()
                };
                dayWorship.WorshipName.IsBold = row.IsNameBold;
                dayWorship.WorshipName.AddOrUpdate("cs-ru", row.Name);

                //DayWorship dayWorship = new DayWorship()
                //{
                //    WorshipName = new ItemFakeText() { StringExpression = row.Name }
                //};

                TriodionDay day = new TriodionDay()
                {
                    //Name = row.Name,
                    //DayName = itemTextCol,
                    DaysFromEaster = (int) row.DayFromEaster,
                };

                day.AppendDayService(dayWorship);
                //day.Sign = _unitOfWork.Repository<Sign>().Get(c => c.Id == row.SignID);

                _unitOfWork.Repository<TriodionDay>().Add(day);

                string folderPath = Path.Combine(Properties.Settings.Default.FolderPath, typiconEntity.Name, "Triodion");

                FileReader fileReader = new FileReader(folderPath);

                TriodionRule rule = new TriodionRule()
                {
                    //Name = day.Name,
                    DaysFromEaster = day.DaysFromEaster,
                    Owner = typiconEntity,
                    //IsAddition = true,
                    Template = typiconEntity.Signs.First(c => c.SignName.FirstOrDefault("cs-ru").Text == row.ServiceSignsRow.Name),
                    RuleDefinition = fileReader.Read(row.DayFromEaster.ToString()),
                    
                };
                rule.DayRuleWorships = new List<DayRuleWorship>() { new DayRuleWorship() { DayRule = rule, DayWorship = dayWorship } };

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

            string folderPath = Path.Combine(Properties.Settings.Default.FolderPath, typiconEntity.Name, "Common");

            FileReader fileReader = new FileReader(folderPath);

            IEnumerable<(string name, string content)> files = fileReader.ReadAllFromDirectory();

            foreach ((string name, string content) file in files)
            {
                CommonRule commonRule = new CommonRule()
                {
                    Name = file.name,
                    RuleDefinition = file.content,
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
                _unitOfWork.Repository<EasterItem>().Add(new EasterItem { Date = row.Date });
            }
            
            //_unitOfWork.Repository<EasterStorage>().Insert(EasterStorage.Instance);
        }
    }
}
