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
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Наследник базового класса. Переопределяет правило для исполнения
    /// </summary>
    public class CustomScheduleDataCalculator : IScheduleDataCalculator
    {
        public CustomScheduleDataCalculator(IRuleSerializerRoot ruleSerializer
            , IRuleHandlerSettingsFactory settingsFactor
            , IScheduleDataCalculator decoratee) 
        {
            RuleSerializer = ruleSerializer ?? throw new ArgumentNullException(nameof(ruleSerializer));
            Decoratee = decoratee ?? throw new ArgumentNullException(nameof(decoratee));
        }

        public string CustomRule { get; set; }

        protected IRuleSerializerRoot RuleSerializer { get; }

        protected IScheduleDataCalculator Decoratee { get; }

        /// <summary>
        /// Переопределяет правило для исполнения
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<ScheduleDataCalculatorResponse> Calculate(ScheduleDataCalculatorRequest request)
        {
            var response = Decoratee.Calculate(request);

            if (response.Failure)
            {
                return response;
            }

            response.Value.Settings.RuleContainer = RuleSerializer.Container<RootContainer>().Deserialize(CustomRule);

            //обнуляем добавления?
            response.Value.Settings.Addition = null;

            return response;
        }
    }
}
