using System;
using System.Linq;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests
{
    public class RuleHandlerSettingsTestFactory
    {
        //IUnitOfWork unitOfWork;
        TypiconEntity typiconEntity;


        public RuleHandlerSettingsTestFactory()
        {
            var unitOfWork = UnitOfWorkFactory.Create();

            typiconEntity = unitOfWork.Repository<TypiconEntity>().Get(c => c.Id == 1);
        }

        public RuleHandlerSettings CreateSettings(int typiconId, DateTime date, string ruleDefinition, RuleHandlerSettings addition = null)
        {
            var menologyRule = DataQueryProcessorFactory.Instance.Process(new MenologyRuleQuery(1, date));
            var oktoikhDay = DataQueryProcessorFactory.Instance.Process(new OktoikhDayQuery(date));

            var ruleContainer = TestRuleSerializer.Deserialize<RootContainer>(ruleDefinition);// menologyRule.GetRule<ExecContainer>(TestRuleSerializer.Root);

            return new RuleHandlerSettings
            {
                TypiconId = typiconId,
                Date = date,
                Addition = addition,
                DayWorships = menologyRule.DayWorships.ToList(),
                OktoikhDay = oktoikhDay,
                RuleContainer = ruleContainer
            };
        }

        public static RuleHandlerSettings Create(int typiconId, DateTime date, string ruleDefinition, RuleHandlerSettings addition = null)
        {
            return new RuleHandlerSettingsTestFactory().CreateSettings(typiconId, date, ruleDefinition, addition);
        }
    }
}
