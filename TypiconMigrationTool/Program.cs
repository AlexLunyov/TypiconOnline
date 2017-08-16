using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using ScheduleHandling;
using System;
using TypiconMigrationTool.Experiments;
using System.Collections.Generic;
using System.Globalization;
using TypiconOnline.Domain.Schedule;
using System.Text;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Easter;
using TypiconOnline.Domain.ViewModels;

namespace TypiconMigrationTool
{
    class Program
    {
        static void Main(string[] args)
        {

            //try
            //{
            var container = new RegisterByContainer().Container;

            var unitOfWork = container.GetInstance<IUnitOfWork>();

            while (true)
            {
                Console.WriteLine("Что желаете? '1' - миграция БД, '3' - тест, '4' - тест для Благовещения, '5' - загрузка TestTypicon");

                ConsoleKeyInfo info = Console.ReadKey();

                switch (info.KeyChar)
                {
                    case '1':
                        Migrate(unitOfWork);
                        break;
                    case '3':
                        TestDate(unitOfWork);
                        break;
                    case '4':
                        TestBlagoveshenie(unitOfWork);
                        break;
                    case '5':
                        //TestMigrate();
                        break;
                }
            }

            
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //} 


            //FillDB(unitOfWork);

            //unitOfWork.Commit();
        }

        private static void TestBlagoveshenie(IUnitOfWork unitOfWork)
        {
            RuleHandler ruleHandler = new RuleHandler(unitOfWork);

            while (true)
            {
                Console.WriteLine("Введите число дней от Пасхи для дня 40 мучеников.");//Благовещения");
                string line = Console.ReadLine();

                int daysFromEaster;

                if (int.TryParse(line, out daysFromEaster))
                {
                    Random random = new Random();

                    DateTime blgvDate = new DateTime(random.Next(2000, 2200), 03, 22);//04, 07);
                    DateTime fakeEaster = blgvDate.AddDays(daysFromEaster*-1);

                    List<EasterItem> easters = new List<EasterItem>()
                    {
                        new EasterItem() { Date = fakeEaster },
                        new EasterItem() { Date = fakeEaster.AddYears(1) }
                    };

                    EasterStorage.Instance.EasterDays = easters;

                    ScheduleDay day = ruleHandler.GetDay(blgvDate);

                    if (day != null)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.AppendLine(day.Date.ToShortDateString() + " " + day.Date.ToString("dddd").ToUpper());
                        stringBuilder.AppendLine(day.Name);
                        foreach (ServiceViewModel service in day.Schedule.ChildElements)
                        {
                            stringBuilder.AppendLine(service.Time + " " + service.Text + " " + service.AdditionalName);
                        }

                        Console.WriteLine(stringBuilder.ToString());
                    }
                }
            }
        }

        private static void TestDate(IUnitOfWork unitOfWork)
        {
            RuleHandler ruleHandler = new RuleHandler(unitOfWork);

            while (true)
            {
                Console.WriteLine("Введите дату в формате дд-мм-гггг");
                string line = Console.ReadLine();

                DateTime date;

                if (DateTime.TryParseExact(line, "dd-MM-yyyy", new CultureInfo("ru-RU"), DateTimeStyles.None, out date))
                {
                    ScheduleDay day = ruleHandler.GetDay(date);

                    if (day != null)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.AppendLine(day.Date.ToShortDateString());
                        stringBuilder.AppendLine(day.Name);
                        foreach (ServiceViewModel service in day.Schedule.ChildElements)
                        {
                            stringBuilder.AppendLine(service.Time + " " + service.Text + " " + service.AdditionalName);
                        }

                        Console.WriteLine(stringBuilder.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Неверный формат даты.");
                }
            }
        }

        private static void Migrate(IUnitOfWork unitOfWork)
        {
            ScheduleHandler sh = new ScheduleHandler("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\data\\ScheduleDB.mdb;");

            Migration migration = new Migration(unitOfWork, sh);

            migration.Execute();
        }

        //private static void FillDB(IUnitOfWork unitOfWork)
        //{
        //    TypiconEntity typiconEntity = new TypiconEntity();
        //    typiconEntity.Name = "Типовой устав";

        //    Sign sign = new Sign()
        //    {
        //        Id = 1,
        //        Priority = 1,
        //        Name = "Без знака",
        //        Owner = typiconEntity
        //    };
        //    unitOfWork.Repository<Sign>().Insert(sign);

        //    typiconEntity.Signs.Add(sign);

        //    MenologyDay menologyDay = new MenologyDay()
        //    {
        //        Id = 1,
        //        Name = "Сщмч. Артемона",
        //        Date = new ItemDate("--04-26"),
        //        DateB = new ItemDate("--04-26"),
        //        //Sign = sign
        //    };
        //    unitOfWork.Repository<MenologyDay>().Insert(menologyDay);

        //    unitOfWork.Repository<TypiconEntity>().Insert(typiconEntity);
            
        //}
    }
}
