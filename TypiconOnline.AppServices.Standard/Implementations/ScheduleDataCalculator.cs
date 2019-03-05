﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Mapster;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.AppServices.Implementations
{
    public class ScheduleDataCalculator : IScheduleDataCalculator
    {
        public ScheduleDataCalculator(IRuleSerializerRoot ruleSerializer
            , IModifiedRuleService modifiedRuleService
            , ITypiconFacade typiconFacade
            , IRuleHandlerSettingsFactory settingsFactory)
        {
            RuleSerializer = ruleSerializer ?? throw new ArgumentNullException(nameof(ruleSerializer));
            ModifiedRuleService = modifiedRuleService ?? throw new ArgumentNullException(nameof(modifiedRuleService));
            TypiconFacade = typiconFacade ?? throw new ArgumentNullException(nameof(typiconFacade));
            SettingsFactory = settingsFactory ?? throw new ArgumentNullException(nameof(settingsFactory));
        }

        protected IModifiedRuleService ModifiedRuleService { get; }
        protected IRuleSerializerRoot RuleSerializer { get; }

        protected ITypiconFacade TypiconFacade { get; }
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
            var menologyRule = TypiconFacade.GetMenologyRule(req.TypiconId, req.Date) ?? throw new NullReferenceException("MenologyRule");
            //var menologyRule = RuleSerializer.QueryProcessor.Process(new MenologyRuleQuery(req.TypiconId, req.Date)) ?? throw new NullReferenceException("MenologyRule");

            //находим TriodionRule
            var triodionRule = TypiconFacade.GetTriodionRule(req.TypiconId, req.Date);
            //var triodionRule = RuleSerializer.QueryProcessor.Process(new DayRuleFromTriodionQuery(req.TypiconId, req.Date));

            //находим ModifiedRule с максимальным приоритетом
            var modifiedRule = TypiconFacade.GetModifiedRuleHighestPriority(req.TypiconId, req.Date);
            //var modifiedRule = ModifiedRuleService.GetModifiedRuleHighestPriority(req.TypiconId, req.Date);

            //находим день Октоиха - не может быть null
            var oktoikhDay = RuleSerializer.QueryProcessor.Process(new OktoikhDayQuery(req.Date)) ?? throw new NullReferenceException("OktoikhDay");

            //создаем выходной объект
            RuleHandlerSettings settings = null;

            //Номер Знака службы, указанный в ModifiedRule, который будет использовать в отображении Расписания
            int? signNumber = null;

            if (modifiedRule != null)
            {
                //custom SignNumber
                signNumber = modifiedRule.SignNumber;

                if (modifiedRule.IsAddition)
                {
                    //создаем первый объект, который в дальнейшем станет ссылкой Addition у выбранного правила
                    var dayRule = modifiedRule.DayRule;//.Adapt<DayRuleDto>();

                    settings = SettingsFactory.Create(new CreateRuleSettingsRequest(req)
                    {
                        Rule = dayRule,
                        DayWorships = dayRule.DayWorships,
                        OktoikhDay = oktoikhDay
                    });

                    //обнуляем его, чтобы больше не участвовал в формировании
                    modifiedRule = null;
                }
            }

            //получаем главное Правило и коллекцию богослужебных текстов
            (DayRule Rule, IEnumerable<DayWorship> Worships) = CalculatePriorities(modifiedRule, menologyRule, triodionRule);

            //смотрим, не созданы ли уже настройки
            if (settings != null)
            {
                //созданы - значит был определен элемент для добавления
                settings.DayWorships.AddRange(Worships);
            }

            RuleHandlerSettings outputSettings = SettingsFactory.Create(new CreateRuleSettingsRequest(req)
            {
                Rule = Rule,
                DayWorships = Worships,
                OktoikhDay = oktoikhDay,
                AdditionalSettings = settings,
                SignNumber = signNumber
            });

            return new ScheduleDataCalculatorResponse() { Rule = Rule, Settings = outputSettings };
        }

        /// <summary>
        /// Из трех правил выбирает главное и составляет коллекцию богослужебных текстов.
        /// Считаем, что ModifiedRule не является ДОПОЛНЕНИЕМ 
        /// </summary>
        /// <param name="modifiedRule"></param>
        /// <param name="menologyRule"></param>
        /// <param name="triodionRule"></param>
        /// <returns>Правило для обработки, список текстов богослужений</returns>
        private (DayRule, IEnumerable<DayWorship>) CalculatePriorities(ModifiedRule modifiedRule, DayRule menologyRule, DayRule triodionRule)
        {
            //Приоритет Минеи
            IDayRule menologyToCompare = SetValues(menologyRule, out int menologyPriority, typeof(MenologyRule));
            //Приоритет Триоди
            IDayRule triodionToCompare = SetValues(triodionRule, out int triodionPriority, typeof(TriodionRule));

            IDayRule SetValues(DayRule dr, out int p, Type t)
            {
                DayRule r = null;
                p = int.MaxValue;

                if (modifiedRule?.DayRule.GetType().Equals(t) == true)
                {
                    r = modifiedRule.DayRule;
                    p = modifiedRule.Priority;
                }
                else if (dr != null)
                {
                    r = dr;
                    p = dr.Template.Priority;
                }

                return r;
            };

            var rulesList = new List<IDayRule>();

            int result = menologyPriority - triodionPriority;
            //сравниваем
            switch (result)
            {
                case 1:
                case 0:
                    //senior Triodion, junior Menology
                    rulesList.Add(triodionToCompare);
                    rulesList.Add(menologyToCompare);
                    break;
                case -1:
                    //senior Menology, junior Triodion
                    rulesList.Add(menologyToCompare);
                    rulesList.Add(triodionToCompare);
                    break;
                default:
                    if (result < -1)
                    {
                        //только Минея
                        rulesList.Add(menologyToCompare);
                    }
                    else
                    {
                        //только Триодь
                        rulesList.Add(triodionToCompare);
                    }
                    break;
            }

            //формируем список текстов
            var dayWorships = new List<DayWorship>();
            rulesList.ForEach(c => dayWorships.AddRange(c.DayWorships));

            //находим главное правило
            var rule = rulesList.First();
            //если это измененное правило, то возвращаем правило, на которое оно указывает
            if (rule is ModifiedRule)
            {
                rule = (rule as ModifiedRule).DayRule;
            }

            return (rule as DayRule, dayWorships);
        }
    }
}
