using System;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.Elements;

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

        public void Import()
        {
            MigrateDayTheotokions(TheotokionAppPlace.app1_kekragaria, DayOfWeek.Sunday);
            MigrateDayTheotokions(TheotokionAppPlace.app1_aposticha, DayOfWeek.Sunday);

            MigrateWeekTheotokions(TheotokionAppPlace.app2_esperinos);
            MigrateWeekTheotokions(TheotokionAppPlace.app2_orthros);

            MigrateWeekTheotokions(TheotokionAppPlace.app3);

            MigrateWeekTheotokions(TheotokionAppPlace.app4_esperinos);
            MigrateWeekTheotokions(TheotokionAppPlace.app4_orthros);
        }

        private void MigrateWeekTheotokions(TheotokionAppPlace source)
        {
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                MigrateDayTheotokions(source, day);
            }
        }

        /// <summary>
        /// Воскресные богородичны первого приложения
        /// </summary>
        private void MigrateDayTheotokions(TheotokionAppPlace source, DayOfWeek day)
        {
            AddTheotokionRequest request = new AddTheotokionRequest();

            for (int ihos = 1; ihos <= 8; ihos++)
            {
                request.Theotokion = Factory.Create(source, ihos, day, FileReader.Read(source, ihos, day));

                Service.Add(request);
            }
        }

        public void Export()
        {
            throw new NotImplementedException();
        }
    }
}
