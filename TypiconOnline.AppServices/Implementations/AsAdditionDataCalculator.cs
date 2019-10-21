using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Добавляет к вычисленным настройкам ModifiedRule, елси оно отмечено как AsAddition
    /// </summary>
    public class AsAdditionDataCalculator : ScheduleDataCalculatorBase
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IScheduleDataCalculator _innerCalculator;
        private readonly IRuleHandlerSettingsFactory _settingsFactory;

        public AsAdditionDataCalculator(IQueryProcessor queryProcessor
            , IScheduleDataCalculator innerCalculator
            , IRuleHandlerSettingsFactory settingsFactory)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _innerCalculator = innerCalculator ?? throw new ArgumentNullException(nameof(innerCalculator));
            _settingsFactory = settingsFactory ?? throw new ArgumentNullException(nameof(settingsFactory));
        }

        public override ScheduleDataCalculatorResponse Calculate(ScheduleDataCalculatorRequest request)
        {
            var result = _innerCalculator.Calculate(request);

            if (result.Exception != null)
            {
                return result;
            }

            //находим ModifiedRule с максимальным приоритетом
            var modifiedRule = _queryProcessor.Process(new ModifiedRuleIsAdditionQuery(request.TypiconVersionId, request.Date));

            if (modifiedRule?.IsAddition == true)
            {
                //создаем первый объект, который в дальнейшем станет ссылкой Addition у выбранного правила
                var dayRule = modifiedRule.DayRule;

                var settings = _settingsFactory.CreateRecursive(new CreateRuleSettingsRequest(request)
                {
                    Rule = dayRule,
                    SignNumber = modifiedRule.SignNumber
                });

                //добавляем DayWorships
                if (TypeEqualsOrSubclassOf<MenologyRule>.Is(modifiedRule.DayRule))
                {
                    settings.Menologies.AddRange(modifiedRule.DayWorships);
                }
                else
                {
                    settings.Triodions.AddRange(modifiedRule.DayWorships);
                }

                //вставляем тексты служб в полученные ранее настройки
                var lastAdditionSettings = FillWorships(settings, result.Settings, false);

                //и задаем результат у последнего найденного Addition
                settings.Menologies = lastAdditionSettings.Menologies;
                settings.Triodions = lastAdditionSettings.Triodions;
                settings.OktoikhDay = lastAdditionSettings.OktoikhDay;

                //теперь дублируем тексты служб на Additions, вычисленные для данных настроек
                if (settings.Addition != null)
                {
                    FillWorships(settings, settings.Addition, true);
                }

                //замыкаем цепочку
                lastAdditionSettings.Addition = settings;

            }

            return result;
        }
    }
}
