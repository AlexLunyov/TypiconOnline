﻿using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Создает настройки из TriodionRule, если оно отмечено как IsTransparent
    /// </summary>
    public class TransparentDataCalculator : ScheduleDataCalculatorBase
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IScheduleDataCalculator _innerCalculator;
        private readonly IRuleHandlerSettingsFactory _settingsFactory;

        public TransparentDataCalculator(IQueryProcessor queryProcessor
            , IScheduleDataCalculator innerCalculator
            , IRuleHandlerSettingsFactory settingsFactory)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _innerCalculator = innerCalculator ?? throw new ArgumentNullException(nameof(innerCalculator));
            _settingsFactory = settingsFactory ?? throw new ArgumentNullException(nameof(settingsFactory));
        }

        public override Result<ScheduleDataCalculatorResponse> Calculate(ScheduleDataCalculatorRequest req)
        {
            var result = _innerCalculator.Calculate(req);

            if (result.Failure)
            {
                return result;
            }

            //находим TriodionRule
            var triodionRule = _queryProcessor.Process(new TriodionRuleQuery(req.TypiconVersionId, req.Date));

            //находим ModifiedRule с максимальным приоритетом
            var modifiedRule = _queryProcessor.Process(new ModifiedRuleHighestPriorityQuery(req.TypiconVersionId, req.Date));

            //формируем, если нет ModifiedRule или оно отмечено как дополнение
            if ((modifiedRule == null || modifiedRule.IsAddition) && triodionRule?.IsTransparent == true)
            {
                var create = _settingsFactory.CreateRecursive(new CreateRuleSettingsRequest(req)
                {
                    Rule = triodionRule,
                    Triodions = triodionRule.DayWorships
                });

                if (create.Success && create.Value is RuleHandlerSettings settings)
                {
                    //задаем результат из ранее вычисленного правила, кроме Triodions
                    settings.Menologies = result.Value.Settings.Menologies;
                    settings.OktoikhDay = result.Value.Settings.OktoikhDay;

                    //теперь дублируем тексты служб на Additions, вычисленные для данных настроек
                    if (settings.Addition != null)
                    {
                        FillWorships(settings, settings.Addition, true);
                    }

                    //добавялем в цепочку Дополнений вычисленные здесь настройки
                    var lastAddition = GetLastAddition(result.Value.Settings);
                    lastAddition.Addition = settings;
                }
            }

            return result;
        }
    }
}
