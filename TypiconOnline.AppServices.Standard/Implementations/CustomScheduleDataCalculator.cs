using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Наследник базового класса. Переопределяет правило для исполнения
    /// </summary>
    public class CustomScheduleDataCalculator : ScheduleDataCalculator
    {
        public CustomScheduleDataCalculator(IRuleSerializerRoot ruleSerializer
            , IModifiedRuleService modifiedRuleService
            , IRuleHandlerSettingsFactory settingsFactory) 
            : base(ruleSerializer, modifiedRuleService, settingsFactory) { }

        public string CustomRule { get; set; }

        /// <summary>
        /// Переопределяет правило для исполнения
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override ScheduleDataCalculatorResponse Calculate(ScheduleDataCalculatorRequest request)
        {
            var response = base.Calculate(request);

            response.Settings.RuleContainer = RuleSerializer.Container<RootContainer>().Deserialize(CustomRule);

            return response;
        }
    }
}
