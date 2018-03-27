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
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.ViewModels;
using TypiconMigrationTool.Experiments.XmlSerialization;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Rules.Days;
using System.IO;
using System.Xml.Serialization;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Repository.EF;
using System.Configuration;
using TypiconOnline.Repository.EFCore;

namespace TypiconMigrationTool
{
    class Program
    {
        static void Main(string[] args)
        {

            //try
            //{
            var container = new RegisterByContainer().Container;

            //var unitOfWork = container.GetInstance<IUnitOfWork>();

            while (true)
            {
                Console.WriteLine("Что желаете? '1' - миграция БД, '3' - тест, '4' - тест для Благовещения, '5' - XmlSerialization");
                Console.WriteLine("'6' - test Esperinos");

                ConsoleKeyInfo info = Console.ReadKey();

                switch (info.KeyChar)
                {
                    case '1':
                        var ef = GetMSSqlUnitOfWork();
                        Migrate(ef);
                        break;
                    case '2':
                        var sqlite = new SQLiteUnitOfWork(@"data\SQLiteDB.db");
                        Migrate(sqlite);
                        break;
                    case '3':
                        ef = GetMSSqlUnitOfWork();
                        TestDate(ef);
                        break;
                    case '4':
                        ef = GetMSSqlUnitOfWork();
                        TestBlagoveshenie(ef);
                        break;
                    case '5':
                        TestXmlSrialization();
                        break;
                    case '6':
                        TestEsperinos();
                        break;
                }

                IUnitOfWork GetMSSqlUnitOfWork()
                {
                    return new MSSqlUnitOfWork(ConfigurationManager.ConnectionStrings["DBTypicon"].ConnectionString);
                };
            }

            
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //} 


            //FillDB(unitOfWork);

            //unitOfWork.Commit();
        }

        private static void TestEsperinos()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Esperinos));

            string xmlString = File.ReadAllText(@"data\Esperinos.xml");

            TypiconSerializer ser = new TypiconSerializer();

            Esperinos esperinos = ser.Deserialize<Esperinos>(xmlString);

            using (FileStream fs = new FileStream(@"data\Esperinos.xml", FileMode.Create))
            {
                formatter.Serialize(fs, esperinos);
                Console.WriteLine("Объект сериализован");
            }
        }

        private static void TestXmlSrialization()
        {
            SerializationExperiment exp = new SerializationExperiment();
            Monastery mon = exp.GetObject();

            string xml = exp.Serialize(mon);
            Console.WriteLine(xml);

            mon = exp.Deserialize<Monastery>(xml);

            Console.WriteLine(mon != null ? "Success." : "Failure.");
        }

        private static void TestBlagoveshenie(IUnitOfWork unitOfWork)
        {
            /*RuleHandler ruleHandler = new RuleHandler(unitOfWork);

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
            }*/
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
                        foreach (WorshipRuleViewModel service in day.Schedule)
                        {
                            stringBuilder.AppendLine(service.Time + " " + service.Name + " " + service.AdditionalName);
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

    }
}
