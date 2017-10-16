using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;

namespace TypiconOnline.AppServices.Migration
{
    public class OktoikhDayMigrationManager : IMigrationManager
    {
        public IOktoikhDayFactory Factory { get; private set; }
        public IOktoikhDayFileReader FileReader { get; private set; }
        public IOktoikhDayService Service { get; private set; }

        public OktoikhDayMigrationManager(IOktoikhDayFactory factory,
                                                    IOktoikhDayFileReader fileReader,
                                                    IOktoikhDayService service)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (fileReader == null) throw new ArgumentNullException("fileReader");
            if (service == null) throw new ArgumentNullException("service");

            Factory = factory;
            FileReader = fileReader;
            Service = service;
        }

        public void Migrate()
        {
            for (int ihos = 1; ihos <= 8; ihos++)
            {
                MigrateWeek(ihos);
            }
        }

        private void MigrateWeek(int ihos)
        {
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                MigrateDay(ihos, day);
            }
        }

        private void MigrateDay(int ihos, DayOfWeek day)
        {
            InsertOktoikhRequest request = new InsertOktoikhRequest()
            {
                OktoikhDay = Factory.Create(ihos, day, FileReader.Read(ihos, day))
            };

            Service.InsertOktoikh(request);
        }
    }
}
