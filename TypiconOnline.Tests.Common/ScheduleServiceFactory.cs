using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Tests.Common
{
    public class ScheduleServiceFactory
    {
        public static ScheduleService Create() => Create(UnitOfWorkFactory.Create());

        public static ScheduleService Create(IUnitOfWork unitOfWork)
        {
            IRuleSerializerRoot serializerRoot = TestRuleSerializer.Create(unitOfWork);

            var settingsFactory = new RuleHandlerSettingsFactory(serializerRoot);

            var modifiedYearFactory = new ModifiedYearFactory(unitOfWork, serializerRoot, settingsFactory);

            return new ScheduleService(new ScheduleDataCalculator(serializerRoot
                , new ModifiedRuleService(unitOfWork, modifiedYearFactory), settingsFactory)
                , new ScheduleDayNameComposer(serializerRoot.QueryProcessor));
        }
    }
}
