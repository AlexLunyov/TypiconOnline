using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Services;
using TypiconOnline.Domain.Books;
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
