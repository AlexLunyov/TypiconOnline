using System;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconMigrationTool
{
    public class ScheduleServiceFactory
    {
        public static IScheduleService Create(IUnitOfWork unitOfWork, IEasterContext easterContext)
        {
            throw new NotImplementedException();
            //new RuleSerializerRoot(BookStorageFactory.Create());
            //return new ScheduleService(new RuleHandlerSettingsFactory(),  )
        }
    }
}
