using ScheduleHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
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
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TypiconOnline.Repository.EFCore.DataBase;
using Microsoft.Extensions.Configuration;

namespace TypiconMigrationTool.Typicon
{
    public class Migration
    {
        const string DEFAULT_LANGUAGE = "cs-ru";
        const string TYPICON_NAME = "Типикон";

        private readonly TypiconDBContext _dbContext;
        //TODO: аттавизм этот удалить при первой возможности
        private readonly IUnitOfWork _unitOfWork;
        private readonly ScheduleHandler _sh;
        private readonly UserCreationService _userCreationService;

        private readonly string FOLDER_PATH;

        public Migration (IUnitOfWork unitOfWork
            , TypiconDBContext dbContext
            , ScheduleHandler sh
            , UserCreationService userCreationService
            , string folderPath)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _sh = sh;
            _userCreationService = userCreationService;

            FOLDER_PATH = folderPath;
        }

        public async Task Execute()
        {
            Console.WriteLine("Migrate()");

            await Migrate();

            Console.WriteLine("MigrateEasters()");
            MigrateEasters();
            Console.WriteLine("MigrateTheotokionIrmologion()");
            MigrateTheotokionIrmologion();
            Console.WriteLine("MigrateOktoikh()");
            MigrateOktoikh();
            Console.WriteLine("MigrateKatavasia()");
            MigrateKatavasia();
            Console.WriteLine("MigrateWeekDayApp()");
            MigrateWeekDayApp();

            Commit();
        }               

        private async Task Migrate()
        {
            var roleAdmin = new Role() { Name = "Администратор", SystemName = "admin" };
            _dbContext.Set<Role>().Add(roleAdmin);
            var roleEditor = new Role() { Name = "Уставщик", SystemName = "editor" };
            _dbContext.Set<Role>().Add(roleEditor);
            var roleTypesetter = new Role() { Name = "Редактор", SystemName = "typesetter" };
            _dbContext.Set<Role>().Add(roleTypesetter);

            Commit();

            var user = new User()
            {
                UserName = "Administrator",
                Email = "ftroem@gmail.com",
            };

            //user.UserRoles = new List<UserRole>()
            //{
            //    new UserRole() { Role = roleAdmin, User = user },
            //    new UserRole() { Role = roleEditor, User = user },
            //    new UserRole() { Role = roleTypesetter, User = user }
            //};

            await _userCreationService.CreateUser(user, "eCa6?&OpM/");

            var typicon = new TypiconEntity()
            {
                Owner = user
            };

            _dbContext.Set<TypiconEntity>().Add(typicon);
            Commit();

            var typiconEntity = new TypiconVersion()
            {
                Name = new ItemText()
                {
                    Items = new List<ItemTextUnit>() { new ItemTextUnit("cs-ru", "Типикон") }
                },
                DefaultLanguage = DEFAULT_LANGUAGE,
                TypiconId = typicon.Id,

                //Делаем сразу опубликованную версию
                BDate = DateTime.Now
            };

            typicon.Versions.Add(typiconEntity);
            Commit();

            string folderPath = Path.Combine(FOLDER_PATH, TYPICON_NAME, "Sign"); 

            FileReader fileReader = new FileReader(folderPath);

            int i = 1;

            foreach (ScheduleDBDataSet.ServiceSignsRow row in _sh.DataSet.ServiceSigns.Rows)
            {
                SignMigrator signMigrator = new SignMigrator(row.Number);

                Sign sign = new Sign()
                {
                    //Id = signMigrator.NewId,
                    Priority = signMigrator.Priority,
                    TypiconVersionId = typiconEntity.Id,
                    //Owner = typiconEntity,
                    IsTemplate = row.IsTemplate,
                    RuleDefinition = fileReader.Read(row.Name),
                    ModRuleDefinition = fileReader.Read(row.Name, "mod")
                };
                sign.SignName.AddOrUpdate(DEFAULT_LANGUAGE, row.Name);

                if (signMigrator.Number != null)
                {
                    sign.Number = (int)signMigrator.Number;
                }

                sign.SignName.AddOrUpdate(DEFAULT_LANGUAGE, row.Name);

                if (signMigrator.TemplateId != null)
                {
                    sign.Template = typiconEntity.Signs.First(c => c.Number == signMigrator.TemplateId);
                    sign.IsAddition = true;
                }

                typiconEntity.Signs.Add(sign);

                i++;
            }

            Commit();

            MigrateMenologyDaysAndRules(typiconEntity);
            Commit();

            MigrateTriodionDaysAndRules(typiconEntity);
            Commit();

            MigrateCommonRules(typiconEntity);
            Commit();

            MigrateExplicitRules(typiconEntity);
            Commit();

            MigratePsalms();
            MigrateKathismas(typiconEntity);            
        }

        private void MigrateExplicitRules(TypiconVersion typiconEntity)
        {
            Console.WriteLine("MigrateExplicitRules()");

            Timer timer = new Timer();
            timer.Start();

            string folderPath = Path.Combine(FOLDER_PATH, TYPICON_NAME, "Explicit");

            FileReader fileReader = new FileReader(folderPath);

            IEnumerable<(string name, string content)> files = fileReader.ReadAllFromDirectory();

            foreach ((string name, string content) in files)
            {
                if (DateTime.TryParse(name, out DateTime date))
                {
                    var explicitRule = new ExplicitAddRule()
                    {
                        Date = date,
                        RuleDefinition = content,
                        TypiconVersionId = typiconEntity.Id
                    };
                    typiconEntity.ExplicitAddRules.Add(explicitRule);
                }
            }

            timer.Stop();
            Console.WriteLine(timer.GetStringValue());
        }

        private void MigrateTheotokionIrmologion()
        {
            string folder = Path.Combine(FOLDER_PATH, @"Books\Irmologion\Theotokion");

            ITheotokionAppFileReader reader = new TheotokionAppFileReader(new FileReader(folder));

            ITheotokionAppService service = new TheotokionAppService(_unitOfWork);

            ITheotokionAppFactory factory = new TheotokionAppFactory();

            IMigrationManager manager = new TheotokionAppMigrationManager(factory, reader, service);

            manager.Import();
        }

        private void MigrateOktoikh()
        {
            string folder = Path.Combine(FOLDER_PATH, @"Books\Oktoikh");

            IOktoikhDayFileReader reader = new OktoikhDayFileReader(new FileReader(folder));

            IEasterContext easterContext = new EasterContext(_unitOfWork);

            IOktoikhDayService service = new OktoikhDayService(_unitOfWork, easterContext);

            IOktoikhDayFactory factory = new OktoikhDayFactory();

            IMigrationManager manager = new OktoikhDayMigrationManager(factory, reader, service);

            manager.Import();
        }

        private void MigrateKatavasia()
        {
            string folder = Path.Combine(FOLDER_PATH, @"Books\Katavasia");

            IFileReader reader = new FileReader(folder);

            IKatavasiaService service = new KatavasiaService(_unitOfWork);

            IKatavasiaFactory factory = new KatavasiaFactory();

            IMigrationManager manager = new KatavasiaMigrationManager(factory, reader, service);

            manager.Import();
        }

        private void MigrateWeekDayApp()
        {
            string folder = Path.Combine(FOLDER_PATH, @"Books\WeekDayApp");

            IFileReader reader = new FileReader(folder);

            var service = new WeekDayAppService(_unitOfWork);

            var factory = new WeekDayAppFactory();

            IMigrationManager manager = new WeekDayAppMigrationManager(factory, reader, service);

            manager.Import();
        }

        private void MigratePsalms()
        {
            Console.WriteLine("MigratePsalms()");
            string folder = Path.Combine(FOLDER_PATH, @"Books\Psalter");

            var service = new PsalterService(_unitOfWork);

            var manager = new PsalmsMigrationManager(service, new TypiconSerializer());

            manager.MigratePsalms(new PsalterRuReader(folder, DEFAULT_LANGUAGE));
            Commit();
            manager.MigratePsalms(new PsalterCsReader(folder, "cs-cs"));
            Commit();
        }

        private void MigrateKathismas(TypiconVersion typiconEntity)
        {
            Console.WriteLine("MigrateKathismas(TypiconVersion typiconEntity)");
            string folder = Path.Combine(FOLDER_PATH, @"Books\Psalter");

            var context = new PsalterContext(_unitOfWork);

            var manager = new KathismasMigrationManager(context);
            manager.MigrateKathismas(new PsalterRuReader(folder, DEFAULT_LANGUAGE), typiconEntity);
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void MigrateMenologyDaysAndRules(TypiconVersion typiconEntity)
        {
            Console.WriteLine("MigrateMenologyDaysAndRules()");

            Timer timer = new Timer();
            timer.Start();

            //TypiconFolderEntity folder = new TypiconFolderEntity() { Name = "Минея" };
            //typiconEntity.RulesFolder.AddFolder(folder);

            //TypiconFolderEntity childFolder = new TypiconFolderEntity() { Name = "Минея 1" };

            //folder.AddFolder(childFolder);

            string folderRulePath = Path.Combine(FOLDER_PATH, TYPICON_NAME, "Menology");

            FileReader fileRuleReader = new FileReader(folderRulePath);

            MenologyDay menologyDay = null;
            MenologyRule menologyRule = null;

            MigrationDayWorshipFactory factory = new MigrationDayWorshipFactory(FOLDER_PATH);

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
                menologyDay = _dbContext.Set<MenologyDay>().FirstOrDefault(c => c.LeapDate.Day == d.Day && c.LeapDate.Month == d.Month);
                if (menologyDay == null)
                {
                    //нет - создаем новый день
                    menologyDay = new MenologyDay()
                    {
                        //Name = mineinikRow.Name,
                        //DayName = XmlHelper.CreateItemTextCollection(
                        //    new CreateItemTextRequest() { Text = mineinikRow.Name, Name = "Name" }),
                        Date = (mineinikRow.IsDateNull()) ? new ItemDate() : new ItemDate(mineinikRow.Date.Month, mineinikRow.Date.Day),
                        LeapDate = (mineinikRow.IsDateBNull()) ? new ItemDate() : new ItemDate(mineinikRow.DateB.Month, mineinikRow.DateB.Day),
                    };

                    _dbContext.Set<MenologyDay>().Add(menologyDay);
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
                        Date = new ItemDate(menologyDay.Date),
                        LeapDate = new ItemDate(menologyDay.LeapDate),
                        TypiconVersionId = typiconEntity.Id,
                        //Owner = typiconEntity,
                        //IsAddition = true,
                        Template = typiconEntity.Signs.First(c => c.SignName.FirstOrDefault(DEFAULT_LANGUAGE).Text == mineinikRow.ServiceSignsRow.Name),
                    };

                    menologyRule.DayRuleWorships.Add( new DayRuleWorship() { DayRule = menologyRule, DayWorship = dayWorship, Order = 1 } );

                    typiconEntity.MenologyRules.Add(menologyRule);

                    string n = (!mineinikRow.IsDateBNull())
                                                    ? menologyDay.LeapDate.Expression
                                                    : menologyRule.GetNameByLanguage(DEFAULT_LANGUAGE);

                    //берем xml-правило из файла
                    menologyRule.RuleDefinition = fileRuleReader.Read(n);
                    menologyRule.ModRuleDefinition = fileRuleReader.Read(n, "mod");
                }
                else
                {
                    int lastOrder = menologyRule.DayRuleWorships.Max(c => c.Order);
                    menologyRule.DayRuleWorships.Add(new DayRuleWorship() { DayRule = menologyRule, DayWorship = dayWorship, Order = lastOrder + 1 });
                }
            }

            timer.Stop();
            Console.WriteLine(timer.GetStringValue());
        }

        private void MigrateTriodionDaysAndRules(TypiconVersion typiconEntity)
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
                dayWorship.WorshipName.AddOrUpdate(DEFAULT_LANGUAGE, row.Name);

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
                //day.Sign = _dbContext.Set<Sign>().Get(c => c.Id == row.SignID);

                _dbContext.Set<TriodionDay>().Add(day);

                string folderPath = Path.Combine(FOLDER_PATH, TYPICON_NAME, "Triodion");

                FileReader fileReader = new FileReader(folderPath);

                TriodionRule rule = new TriodionRule()
                {
                    //Name = day.Name,
                    DaysFromEaster = day.DaysFromEaster,
                    TypiconVersionId = typiconEntity.Id,
                    //Owner = typiconEntity,
                    //IsAddition = true,
                    Template = typiconEntity.Signs.First(c => c.SignName.FirstOrDefault(DEFAULT_LANGUAGE).Text == row.ServiceSignsRow.Name),
                    RuleDefinition = fileReader.Read(row.DayFromEaster.ToString()),
                    ModRuleDefinition = fileReader.Read(row.DayFromEaster.ToString(), "mod")

                };
                rule.DayRuleWorships = new List<DayRuleWorship>() { new DayRuleWorship() { DayRule = rule, DayWorship = dayWorship, Order = 1 } };

                //folder.AddRule(rule);
                typiconEntity.TriodionRules.Add(rule);
            }

            timer.Stop();
            Console.WriteLine(timer.GetStringValue());

            //_unitOfWork.Commit();
        }

        private void MigrateCommonRules(TypiconVersion typiconEntity)
        {
            Console.WriteLine("MigrateCommonRules()");

            Timer timer = new Timer();
            timer.Start();

            string folderPath = Path.Combine(FOLDER_PATH, TYPICON_NAME, "Common");

            FileReader fileReader = new FileReader(folderPath);

            IEnumerable<(string name, string content)> files = fileReader.ReadAllFromDirectory();

            foreach ((string name, string content) file in files)
            {
                CommonRule commonRule = new CommonRule()
                {
                    Name = file.name,
                    RuleDefinition = file.content,
                    TypiconVersionId = typiconEntity.Id,
                    //Owner = typiconEntity
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
                _dbContext.Set<EasterItem>().Add(new EasterItem { Date = row.Date });
            }
            
            //_dbContext.Set<EasterStorage>().Insert(EasterStorage.Instance);
        }
    }
}
