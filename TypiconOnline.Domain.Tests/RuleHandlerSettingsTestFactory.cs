using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Tests.Rules;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
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

        public RuleHandlerSettings CreateSettings(DateTime date, string ruleDefinition, RuleHandlerSettings addition = null)
        {
            var menologyRule = typiconEntity.GetMenologyRule(date);
            menologyRule.RuleDefinition = ruleDefinition;

            var bookStorage = BookStorageFactory.Create();
            var oktoikhDay = bookStorage.Oktoikh.Get(date);

            var ruleContainer = menologyRule.GetRule<ExecContainer>(TestRuleSerializer.Root);

            return new RuleHandlerSettings
            {
                Date = date,
                Addition = addition,
                TypiconRule = menologyRule,
                DayWorships = menologyRule.DayWorships,
                OktoikhDay = oktoikhDay,
                RuleContainer = ruleContainer
            };
        }
    }
}
