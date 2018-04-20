using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Наследник базового класса. Переопределяет правило для исполнения
    /// </summary>
    public class CustomRuleSettingsFactory : RuleHandlerSettingsFactory
    {
        public CustomRuleSettingsFactory(IRuleSerializerRoot ruleSerializer, IModifiedRuleService modifiedRuleService) 
            : base(ruleSerializer, modifiedRuleService) { }

        public string CustomRule { get; set; }

        /// <summary>
        /// Переопределяет правило для исполнения
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override RuleHandlerSettings Create(GetRuleSettingsRequest request)
        {
            RuleHandlerSettings result = base.Create(request);

            result.TypiconRule.RuleDefinition = CustomRule;

            return result;
        }
    }
}
