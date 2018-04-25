using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests
{
    public class ScheduleServiceFactory
    {
        public static ScheduleService Create() => Create(UnitOfWorkFactory.Create());

        public static ScheduleService Create(IUnitOfWork unitOfWork)
        {
            var bookStorageFactory = BookStorageFactory.Create();
            IRuleSerializerRoot serializerRoot = new RuleSerializerRoot(bookStorageFactory);


            return new ScheduleService(new RuleHandlerSettingsFactory(serializerRoot, new ModifiedRuleService(unitOfWork))
                                     , new ScheduleDayNameComposer(bookStorageFactory.Oktoikh));
        }
    }
}
