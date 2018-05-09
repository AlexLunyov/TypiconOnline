using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Migration
{
    /// <summary>
    /// Реализует миграцию Богородичных Ирмология
    /// </summary>
    public class TheotokionAppMigrationManager : IMigrationManager
    {
        public ITheotokionAppFactory Factory { get; private set; }
        public ITheotokionAppFileReader FileReader { get; private set; }
        public ITheotokionAppService Service { get; private set; }

        public TheotokionAppMigrationManager(ITheotokionAppFactory factory,
                                                    ITheotokionAppFileReader fileReader,
                                                    ITheotokionAppService service)
        {
            Factory = factory ?? throw new ArgumentNullException("factory");
            FileReader = fileReader ?? throw new ArgumentNullException("fileReader");
            Service = service ?? throw new ArgumentNullException("service");
        }

        public void Migrate()
        {
            MigrateDayTheotokions(PlaceYmnosSource.app1_kekragaria, DayOfWeek.Sunday);
            MigrateDayTheotokions(PlaceYmnosSource.app1_aposticha, DayOfWeek.Sunday);

            MigrateWeekTheotokions(PlaceYmnosSource.app2_esperinos);
            MigrateWeekTheotokions(PlaceYmnosSource.app2_orthros);

            MigrateWeekTheotokions(PlaceYmnosSource.app3);

            MigrateWeekTheotokions(PlaceYmnosSource.app4_esperinos);
            MigrateWeekTheotokions(PlaceYmnosSource.app4_orthros);
        }

        private void MigrateWeekTheotokions(PlaceYmnosSource source)
        {
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                MigrateDayTheotokions(source, day);
            }
        }

        /// <summary>
        /// Воскресные богородичны первого приложения
        /// </summary>
        private void MigrateDayTheotokions(PlaceYmnosSource source, DayOfWeek day)
        {
            AddTheotokionRequest request = new AddTheotokionRequest();

            for (int ihos = 1; ihos <= 8; ihos++)
            {
                request.Theotokion = Factory.Create(source, ihos, day, FileReader.Read(source, ihos, day));

                Service.Add(request);
            }
        }
    }
}
