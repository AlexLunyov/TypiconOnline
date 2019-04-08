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

            var commandProcessor = CommandProcessorFactory.Create(dbContext);
            var queryProcessor = DataQueryProcessorFactory.Create(dbContext);

            return new ScheduleService(new ScheduleDataCalculator(queryProcessor, settingsFactory)
                , new ScheduleDayNameComposer(serializerRoot.QueryProcessor));
        }
    }
}
