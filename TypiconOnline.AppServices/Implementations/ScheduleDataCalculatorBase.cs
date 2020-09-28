using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Implementations
{
    public abstract class ScheduleDataCalculatorBase : IScheduleDataCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="own">Свои сгенерированные настройки</param>
        /// <param name="external">Внешние от <see cref="_innerCalculator"/></param>
        /// <param name="oktoikhMode">Если true - День Октоиха назначается для external, иначе для own</param>
        /// <returns>Самый последний Addition у <see cref="own"/></returns>
        protected RuleHandlerSettings FillWorships(RuleHandlerSettings own, RuleHandlerSettings external, bool oktoikhMode)
        {
            external.Menologies.AddRange(own.Menologies);
            external.Triodions.AddRange(own.Triodions);

            //external.Menologies.InsertRange(0, own.Menologies);
            //external.Triodions.InsertRange(0, own.Triodions);

            if (oktoikhMode)
            {
                external.OktoikhDay = own.OktoikhDay;
            }
            else
            {
                own.OktoikhDay = external.OktoikhDay;
            }

            var lastAddition = external;

            if (external.Addition != null)
            {
                lastAddition = FillWorships(own, external.Addition, oktoikhMode);
            }

            return lastAddition;
        }

        protected RuleHandlerSettings GetLastAddition(RuleHandlerSettings settings)
        {
            return (settings.Addition == null) ?
                settings : GetLastAddition(settings.Addition);
        }

        public abstract Result<ScheduleDataCalculatorResponse> Calculate(ScheduleDataCalculatorRequest request);
    }
}
