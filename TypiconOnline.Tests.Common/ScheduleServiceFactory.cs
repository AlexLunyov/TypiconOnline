using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Tests.Common
{
    public class ScheduleServiceFactory
    {
        public static ScheduleService Create() => Create(TypiconDbContextFactory.Create());

        public static ScheduleService Create(TypiconDBContext dbContext)
        {
            IRuleSerializerRoot serializerRoot = TestRuleSerializer.Create(dbContext);

            var settingsFactory = new RuleHandlerSettingsFactory(serializerRoot);

            var uof = UnitOfWorkFactory.Create(dbContext);

            var typiconFacade = new TypiconFromEntityFacade(dbContext);

            var modifiedYearFactory = new ModifiedYearFactory(uof, settingsFactory, typiconFacade);

            return new ScheduleService(new ScheduleDataCalculator(serializerRoot
                    , new ModifiedRuleService(uof, modifiedYearFactory)
                    , typiconFacade
                    , settingsFactory)
                , new ScheduleDayNameComposer(serializerRoot.QueryProcessor));
        }
    }
}
