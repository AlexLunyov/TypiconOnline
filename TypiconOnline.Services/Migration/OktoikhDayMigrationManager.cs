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
            Factory = factory ?? throw new ArgumentNullException("factory");
            FileReader = fileReader ?? throw new ArgumentNullException("fileReader");
            Service = service ?? throw new ArgumentNullException("service");
        }

        public void Import()
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
            AddOktoikhRequest request = new AddOktoikhRequest()
            {
                OktoikhDay = Factory.Create(ihos, day, FileReader.Read(ihos, day))
            };

            Service.Add(request);
        }

        public void Export()
        {
            throw new NotImplementedException();
        }
    }
}
