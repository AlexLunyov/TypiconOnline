using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.Irmologion;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Migration
{
    /// <summary>
    /// Реализует миграцию Богородичных Ирмология
    /// </summary>
    public class IrmologionTheotokionMigrationManager : IMigrationManager
    {
        public IIrmologionTheotokionFactory Factory { get; private set; }
        public IIrmologionTheotokionFileReader FileReader { get; private set; }
        public IIrmologionTheotokionService Service { get; private set; }

        public IrmologionTheotokionMigrationManager(IIrmologionTheotokionFactory factory,
                                                    IIrmologionTheotokionFileReader fileReader,
                                                    IIrmologionTheotokionService service)
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
            InsertTheotokionRequest request = new InsertTheotokionRequest();

            for (int ihos = 1; ihos <= 8; ihos++)
            {
                request.Theotokion = Factory.Create(source, ihos, day, FileReader.Read(source, ihos, day));

                Service.InsertTheotokion(request);
            }
        }
    }
}
