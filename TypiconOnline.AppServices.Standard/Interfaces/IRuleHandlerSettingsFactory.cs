using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IRuleHandlerSettingsFactory
    {
        /// <summary>
        /// Создает настройки для IRuleHandler, рекурсивно проходя по всем Шаблонам указанного Правила
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RuleHandlerSettings CreateRecursive(CreateRuleSettingsRequest request);
        /// <summary>
        /// Создает настройки из ExplicitAddRule
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RuleHandlerSettings CreateExplicit(CreateExplicitRuleSettingsRequest request);
    }
}
