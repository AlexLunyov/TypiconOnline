using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.AppServices.Implementations
{
    public class ScheduleDataCalculator : IScheduleDataCalculator
    {
        public ScheduleDataCalculator(IQueryProcessor queryProcessor, IRuleHandlerSettingsFactory settingsFactory)
        {
            QueryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            SettingsFactory = settingsFactory ?? throw new ArgumentNullException(nameof(settingsFactory));
        }

        
        protected IQueryProcessor QueryProcessor { get; }
        protected IRuleHandlerSettingsFactory SettingsFactory { get; }

        /// <summary>
        /// Формирует данные для дальнейшей обработки.
        /// Выбирает главное правило для обработки и настройки для IRuleHandler.
        /// Процесс описан в документации :)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual ScheduleDataCalculatorResponse Calculate([NotNull] ScheduleDataCalculatorRequest req)
        {
            if (req == null)
            {
                throw new ArgumentNullException("GetRuleSettingsRequest in Create");
            }

            //заполняем Правила и день Октоиха
            //находим MenologyRule - не может быть null
            var menologyRule = QueryProcessor.Process(new MenologyRuleQuery(req.TypiconVersionId, req.Date)) ?? throw new NullReferenceException("MenologyRule");

            //находим TriodionRule
            var triodionRule = QueryProcessor.Process(new TriodionRuleQuery(req.TypiconVersionId, req.Date));

            //находим ModifiedRule с максимальным приоритетом
            var modifiedRule = QueryProcessor.Process(new ModifiedRuleHighestPriorityQuery(req.TypiconVersionId, req.Date));

            //находим ExplicitAddRule
            var explicitAddRule = QueryProcessor.Process(new ExplicitAddRuleQuery(req.TypiconVersionId, req.Date));

            //находим день Октоиха - не может быть null
            var oktoikhDay = QueryProcessor.Process(new OktoikhDayQuery(req.Date)) ?? throw new NullReferenceException("OktoikhDay");

            //Создаем настройки из ExplicitAddRule
            RuleHandlerSettings settings = CreateFromExplicit(req, explicitAddRule);

            //Номер Знака службы, указанный в ModifiedRule, который будет использовать в отображении Расписания
            PrintDayTemplate printDayTemplate = null;

            settings = CreateFromModifiedAsAddition(req, ref modifiedRule, ref printDayTemplate, settings);

            settings = CreateFromTransparent(req, (modifiedRule == null), triodionRule, settings);
            //получаем главное Правило
            var (majorRule, outputSettings) = CreateFromMajor(req, modifiedRule, menologyRule, triodionRule, oktoikhDay, settings, printDayTemplate);

            FillAllSettingsByWorships(outputSettings);

            return new ScheduleDataCalculatorResponse() { Rule = majorRule, Settings = outputSettings };
        }

        private RuleHandlerSettings CreateFromExplicit(ScheduleDataCalculatorRequest req, ExplicitAddRule explicitAddRule)
        {
            return (explicitAddRule != null)
                ? SettingsFactory.CreateExplicit(new CreateExplicitRuleSettingsRequest(req)
                {
                    Rule = explicitAddRule
                })
                : default(RuleHandlerSettings);
        }

        private RuleHandlerSettings CreateFromModifiedAsAddition(ScheduleDataCalculatorRequest req, ref ModifiedRule modifiedRule, ref PrintDayTemplate printDayTemplate, RuleHandlerSettings settings)
        {
            printDayTemplate = null;

            if (modifiedRule != null)
            {
                //custom printDayTemplate
                printDayTemplate = modifiedRule.PrintDayTemplate;

                if (modifiedRule.IsAddition)
                {
                    //создаем первый объект, который в дальнейшем станет ссылкой Addition у выбранного правила
                    var dayRule = modifiedRule.DayRule;

                    settings = SettingsFactory.CreateRecursive(new CreateRuleSettingsRequest(req)
                    {
                        Rule = dayRule,
                        //DayWorships = dayRule.DayWorships,
                        AdditionalSettings = settings
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

                    //обнуляем его, чтобы больше не участвовал в формировании
                    modifiedRule = null;
                }
            }

            return settings;
        }

        /// <summary>
        /// Создает настройки из TriodionRule, если оно отмечено как IsTransparent
        /// </summary>
        /// <param name="req"></param>
        /// <param name="modifiedRule"></param>
        /// <param name="triodionRule"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private RuleHandlerSettings CreateFromTransparent(ScheduleDataCalculatorRequest req, bool modifiedRuleIsNull, TriodionRule triodionRule, RuleHandlerSettings settings)
        {
            //формируем, если нет ModifiedRule
            if (modifiedRuleIsNull && triodionRule?.IsTransparent == true)
            {
                settings = SettingsFactory.CreateRecursive(new CreateRuleSettingsRequest(req)
                {
                    Rule = triodionRule,
                    Triodions = triodionRule.DayWorships,
                    AdditionalSettings = settings
                });
            }

            return settings;
        }

        /// <summary>
        /// Из трех правил выбирает главное и составляет коллекцию богослужебных текстов.
        /// Считаем, что ModifiedRule не является ДОПОЛНЕНИЕМ 
        /// </summary>
        /// <returns>Правило для обработки, настройки для обработчика правил</returns>
        private (DayRule MajorRule, RuleHandlerSettings Settings) CreateFromMajor(ScheduleDataCalculatorRequest req, ModifiedRule modifiedRule, MenologyRule menologyRule, TriodionRule triodionRule
            , OktoikhDay oktoikhDay, RuleHandlerSettings settings, PrintDayTemplate printDayTemplate)
        {
            (DayRule MajorRule, IEnumerable<DayWorship> Menologies, IEnumerable<DayWorship> Triodions) r;

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
            }

            settings = SettingsFactory.CreateRecursive(new CreateRuleSettingsRequest(req)
            {
                Rule = r.MajorRule,
                Menologies = r.Menologies,
                Triodions = r.Triodions,
                OktoikhDay = oktoikhDay,
                AdditionalSettings = settings,
                PrintDayTemplate = printDayTemplate
            });

            return (r.MajorRule, settings);
        }


        /// <summary>
        /// Из трех правил выбирает главное и составляет коллекцию богослужебных текстов.
        /// Считаем, что ModifiedRule не является ДОПОЛНЕНИЕМ 
        /// </summary>
        /// <param name="modifiedRule"></param>
        /// <param name="menologyRule"></param>
        /// <param name="triodionRule"></param>
        /// <returns>Правило для обработки, список текстов богослужений</returns>
        private (DayRule MajorRule, IEnumerable<DayWorship> Menologies, IEnumerable<DayWorship> Triodions) CalculatePriorities(ModifiedRule modifiedRule, MenologyRule menologyRule, TriodionRule triodionRule)
        {
            //Приоритет Минеи
            IDayRule menologyToCompare = SetValues(menologyRule, out int menologyPriority, TypeEqualsOrSubclassOf<MenologyRule>.Is(modifiedRule?.DayRule));
            //Приоритет Триоди
            IDayRule triodionToCompare = SetValues(triodionRule, out int triodionPriority, TypeEqualsOrSubclassOf<TriodionRule>.Is(modifiedRule?.DayRule));

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

            //если это измененное правило, то возвращаем правило, на которое оно указывает
            if (majorRule is ModifiedRule mr)
            {
                majorRule = mr.DayRule;
            }

            return (majorRule as DayRule, menologies, triodions);
        }

        /// <summary>
        /// Рекурсивно добавляет к настройкам коллекции Menologies, Triodions и OktoikhDay
        /// </summary>
        /// <param name="settings"></param>
        private void FillAllSettingsByWorships(RuleHandlerSettings settings)
        {
            //для начала собираем общую коллекцию текстов служб


            if (settings.Addition is RuleHandlerSettings addition)
            {
                addition.Menologies.AddRange(settings.Menologies);
                addition.Triodions.AddRange(settings.Triodions);
                addition.OktoikhDay = settings.OktoikhDay;

                FillAllSettingsByWorships(addition);
            }
        }
    }
}
