using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.AppServices.Implementations
{
    public class MajorDataCalculator : ScheduleDataCalculatorBase
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IRuleHandlerSettingsFactory _settingsFactory;

        public MajorDataCalculator(IQueryProcessor queryProcessor
            , IRuleHandlerSettingsFactory settingsFactory)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _settingsFactory = settingsFactory ?? throw new ArgumentNullException(nameof(settingsFactory));
        }

        public override Result<ScheduleDataCalculatorResponse> Calculate(ScheduleDataCalculatorRequest req)
        {
            //находим MenologyRule - не может быть null
            var menologyRule = _queryProcessor.Process(new MenologyRuleQuery(req.TypiconVersionId, req.Date)) ?? throw new NullReferenceException("MenologyRule");

            //находим TriodionRule
            var triodionRule = _queryProcessor.Process(new TriodionRuleQuery(req.TypiconVersionId, req.Date));

            //находим ModifiedRule с максимальным приоритетом
            var modifiedRule = _queryProcessor.Process(new ModifiedRuleHighestPriorityQuery(req.TypiconVersionId, req.Date));

            //находим день Октоиха - не может быть null
            var oktoikhDay = _queryProcessor.Process(new OktoikhDayQuery(req.Date)) ?? throw new NullReferenceException("OktoikhDay");

            //вычисляем приоритеты, находим главное правило
            (DayRule MajorRule, IEnumerable<DayWorship> Menologies, IEnumerable<DayWorship> Triodions, bool ModifiedIsUsed) r;

            if (modifiedRule != null || triodionRule?.IsTransparent == false)
            {
                r = CalculatePriorities(modifiedRule, menologyRule, triodionRule);
            }
            else
            {
                //если мы здесь, то осталась только Минея
                r.MajorRule = menologyRule;
                r.Menologies = menologyRule.DayWorships;
                r.Triodions = new List<DayWorship>();
                r.ModifiedIsUsed = false;
            }

            var settings = _settingsFactory.CreateRecursive(new CreateRuleSettingsRequest(req)
            {
                Rule = r.MajorRule,
                Menologies = r.Menologies,
                Triodions = r.Triodions,
                OktoikhDay = oktoikhDay,
                PrintDayTemplate = r.ModifiedIsUsed
                    ? modifiedRule?.PrintDayTemplate
                    : null//r.MajorRule.PrintDayTemplate
            });

            if (settings.Success)
            {
                //теперь дублируем тексты служб на Additions, вычисленные для данных настроек
                if (settings.Value.Addition != null)
                {
                    FillWorships(settings.Value, settings.Value.Addition, true);
                }

                return Result.Ok(new ScheduleDataCalculatorResponse() { Rule = r.MajorRule, Settings = settings.Value });
            }
            else
            {
                return Result.Fail<ScheduleDataCalculatorResponse>(settings.ErrorCode, settings.Error);
            }
            
        }

        /// <summary>
        /// Из трех правил выбирает главное и составляет коллекцию богослужебных текстов.
        /// Считаем, что ModifiedRule не является ДОПОЛНЕНИЕМ 
        /// </summary>
        /// <param name="modifiedRule"></param>
        /// <param name="menologyRule"></param>
        /// <param name="triodionRule"></param>
        /// <returns>Правило для обработки, список текстов богослужений</returns>
        private (DayRule MajorRule, IEnumerable<DayWorship> Menologies, IEnumerable<DayWorship> Triodions, bool ModifiedIsUsed) CalculatePriorities(ModifiedRule modifiedRule, MenologyRule menologyRule, TriodionRule triodionRule)
        {            
            //Приоритет Минеи
            IDayRule menologyToCompare = SetValues(menologyRule
                , out int menologyPriority
                , TypeEqualsOrSubclassOf<MenologyRule>.Is(modifiedRule?.DayRule));

            //Приоритет Триоди
            IDayRule triodionToCompare = SetValues(triodionRule
                , out int triodionPriority
                , TypeEqualsOrSubclassOf<TriodionRule>.Is(modifiedRule?.DayRule));

            IDayRule SetValues(DayRule dr, out int p, bool typeEqualsOrSubclassOf)
            {
                IDayRule r = null;
                p = int.MaxValue;

                if (typeEqualsOrSubclassOf)
                {
                    r = modifiedRule;//.DayRule;
                    p = modifiedRule.Priority;
                }
                else if (dr != null)
                {
                    r = dr;
                    p = dr.Template.Priority;
                }

                return r;
            };

            IDayRule majorRule = null;
            var menologies = new List<DayWorship>();
            var triodions = new List<DayWorship>();

            int result = menologyPriority - triodionPriority;
            //сравниваем
            switch (result)
            {
                case 1:
                case 0:
                    //senior Triodion, junior Menology
                    majorRule = triodionToCompare;

                    menologies.AddRange(menologyToCompare.DayWorships);
                    triodions.AddRange(triodionToCompare.DayWorships);
                    break;
                case -1:
                    //senior Menology, junior Triodion
                    majorRule = menologyToCompare;

                    menologies.AddRange(menologyToCompare.DayWorships);
                    triodions.AddRange(triodionToCompare.DayWorships);
                    break;
                default:
                    if (result < -1)
                    {
                        //только Минея
                        majorRule = menologyToCompare;

                        menologies.AddRange(menologyToCompare.DayWorships);
                    }
                    else
                    {
                        //только Триодь
                        majorRule = triodionToCompare;

                        triodions.AddRange(triodionToCompare.DayWorships);
                    }
                    break;
            }

            //Вводим переменную для того, чтобы затем обнулить ссылку на ModifiedRule
            bool modifiedRuleIsUsed = false;

            //если это измененное правило, то возвращаем правило, на которое оно указывает
            if (majorRule is ModifiedRule mr)
            {
                majorRule = mr.DayRule;

                modifiedRuleIsUsed = true;
            }

            return (majorRule as DayRule, menologies, triodions, modifiedRuleIsUsed);
        }
    }
}
