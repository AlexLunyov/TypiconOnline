using System;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;

namespace TypiconOnline.AppServices.Migration
{
    public class WeekDayAppMigrationManager : IMigrationManager
    {
        public IWeekDayAppFactory Factory { get; private set; }
        public IFileReader FileReader { get; private set; }
        public IWeekDayAppService Service { get; private set; }

        public WeekDayAppMigrationManager(IWeekDayAppFactory factory,
                                                    IFileReader fileReader,
                                                    IWeekDayAppService service)
        {
            Factory = factory ?? throw new ArgumentNullException("factory");
            FileReader = fileReader ?? throw new ArgumentNullException("fileReader");
            Service = service ?? throw new ArgumentNullException("service");
        }

        public void Import()
        {
            var request = new AddWeekDayAppRequest();

            foreach ((string name, string content) in FileReader.ReadAllFromDirectory())
            {
                request.WeekDayApp = Factory.Create(name, content);

                Service.Insert(request);
            }
        }

        public void Export()
        {
            throw new NotImplementedException();
        }
    }
}
