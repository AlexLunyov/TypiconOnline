using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.AppServices.Implementations
{
    public class RuleHandlerSettingsFactory : IRuleHandlerSettingsFactory
    {
        //IOktoikhContext _oktoikhContext;
        //TypiconEntity _typicon;
        //IModifiedRuleService _modifiedRuleService;

        public RuleHandlerSettingsFactory()
        {
            //_modifiedRuleService = service ?? throw new ArgumentNullException("IModifiedRuleService");
            //_oktoikhContext = oktoikhContext ?? throw new ArgumentNullException("IOktoikhContext");
            //_typicon = typicon ?? throw new ArgumentNullException("TypiconEntity");
        }

        /// <summary>
        /// Формирует запрос для дальнейшей обработки: главную и второстепенную службу, HandlingMode
        /// Процесс описан в документации
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual RuleHandlerSettings Create(GetRuleSettingsRequest req)
        {
            //MenologyRule - не может быть null
            if (req.MenologyRule == null) throw new NullReferenceException("MenologyRule");
            //находим день Октоиха - не может быть null
            if (req.OktoikhDay == null) throw new NullReferenceException("OktoikhDay");

            //находим TriodionRule
            //TriodionRule triodionRule = req.Typicon.GetTriodionRule(req.Date);

            //находим ModifiedRule с максимальным приоритетом 
            //ModifiedRule modifiedRule = _modifiedRuleService.GetModifiedRuleHighestPriority(req.Typicon, req.Date);

            //создаем выходной объект
            RuleHandlerSettings settings = null;

            //Номер Знака службы, указанный в ModifiedRule, который будет использовать в отображении Расписания
            int? signNumber = null;

            if (req.ModifiedRule != null)
            {
                //custom SignNumber
                signNumber = req.ModifiedRule.SignNumber;

                if (req.ModifiedRule.IsAddition == true)
                {
                    //создаем первый объект, который в дальнейшем станет ссылкой Addition у выбранного правила
                    settings = InnerCreate(req.ModifiedRule.RuleEntity, req.ModifiedRule.RuleEntity.DayWorships, req.OktoikhDay, req, null);

                    //обнуляем его, чтобы больше не участвовал в формировании
                    req.ModifiedRule = null;
                }
            }

            //получаем главное Правило и коллекцию богослужебных текстов
            (DayRule Rule, IEnumerable<DayWorship> Worships) = CalculatePriorities(req.ModifiedRule, req.MenologyRule, req.TriodionRule);

            //смотрим, не созданы ли уже настройки
            if (settings != null)
            {
                //созданы - значит был определен элемент для добавления
                settings.DayWorships.AddRange(Worships);
            }


            TypiconRule seniorRule = Rule;
            //смотрим, главным ставить само Правило или Знак службы
            /*
             * Если Правило имеет: 
             *      пустое RuleDefinition
             *      //и Правило помечено как Дополнение
             *      и определен Шаблон
             */
            if (
                string.IsNullOrEmpty(Rule.RuleDefinition)
                && Rule.IsAddition 
                && Rule.Template != null
                )
            {
                seniorRule = seniorRule.Template;
            }

            RuleHandlerSettings outputSettings = GetRecursiveSettings(seniorRule, Worships, req.OktoikhDay, req, settings, signNumber);

            return outputSettings;
        }

        /// <summary>
        /// Из трех правил выбирает главное и составляет коллекцию богослужебных текстов.
        /// Считаем, что ModifiedRule не является ДОПОЛНЕНИЕМ 
        /// </summary>
        /// <param name="modifiedRule"></param>
        /// <param name="menologyRule"></param>
        /// <param name="triodionRule"></param>
        /// <returns>Правило для обработки, список текстов богослужений</returns>
        private (DayRule, IEnumerable<DayWorship>) CalculatePriorities(ModifiedRule modifiedRule, MenologyRule menologyRule, TriodionRule triodionRule)
        {
            //Приоритет Минеи
            IDayRule menologyToCompare = SetValues(menologyRule, out int menologyPriority, typeof(MenologyRule));
            //Приоритет Триоди
            IDayRule triodionToCompare = SetValues(triodionRule, out int triodionPriority, typeof(TriodionRule));

            IDayRule SetValues(DayRule dr, out int p, Type t)
            {
                IDayRule r = null;
                p = int.MaxValue;
                if (modifiedRule?.RuleEntity.GetType().Equals(t) == true)
                {
                    r = modifiedRule;
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
            List<DayWorship> dayWorships = new List<DayWorship>();
            rulesList.ForEach(c => dayWorships.AddRange(c.DayWorships));

            //находим главное правило
            var rule = rulesList.First();
            //если это измененное правило, то возвращаем правило, на которое оно указывает
            if (rule is ModifiedRule)
            {
                rule = (rule as ModifiedRule).RuleEntity;
            }

            return (rule as DayRule, dayWorships);
        }

        private RuleHandlerSettings GetRecursiveSettings(TypiconRule rule, IEnumerable<DayWorship> dayWorships, OktoikhDay oktoikhDay,
            GetRuleSettingsRequest req, RuleHandlerSettings additionalSettings, int? signNumber = null)
        {
            RuleHandlerSettings outputSettings = InnerCreate(rule, dayWorships, oktoikhDay, req, additionalSettings, signNumber);

            /*
             * Если Правило имеет: 
             *      Правило помечено как Дополнение
             *      и определен Шаблон
             */

            if (/*string.IsNullOrEmpty(rule.RuleDefinition) && */rule.IsAddition && rule.Template != null)
            {
                //если правило определено и определено как добавление, входим в рекурсию
                outputSettings = GetRecursiveSettings(rule.Template, dayWorships, oktoikhDay, req, outputSettings);
            }

            return outputSettings;
        }

        private RuleHandlerSettings InnerCreate(TypiconRule rule, IEnumerable<DayWorship> dayWorships, OktoikhDay oktoikhDay, 
            GetRuleSettingsRequest req, RuleHandlerSettings additionalSettings, int? signNumber = null)
        {
            return new RuleHandlerSettings()
            {
                Addition = additionalSettings,
                Date = req.Date,
                Rule = rule,
                DayWorships = dayWorships.ToList(),
                OktoikhDay = oktoikhDay,
                Language = LanguageSettingsFactory.Create(req.Language),
                SignNumber = signNumber,
                //ThrowExceptionIfInvalid = req.ThrowExceptionIfInvalid,
                ApplyParameters = req.ApplyParameters,
                CheckParameters = req.CheckParameters
            };
        }
    }
}
