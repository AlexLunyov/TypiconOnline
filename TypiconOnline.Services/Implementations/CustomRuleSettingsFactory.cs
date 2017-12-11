using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Наследник базового класса. Переопределяет правило для исполнения
    /// </summary>
    public class CustomRuleSettingsFactory : RuleHandlerSettingsFactory
    {
        public string CustomRule { get; set; }

        /// <summary>
        /// Переопределяет правило для исполнения
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override RuleHandlerSettings Create(GetRuleSettingsRequest request)
        {
            RuleHandlerSettings result = base.Create(request);

            result.Rule.RuleDefinition = CustomRule;

            return result;
        }
    }
}
