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

            if (req.ModifiedRule?.IsAddition == true)
            {
                //создаем первый объект, который в дальнейшем станет ссылкой Addition у выбранного правила
                settings = Create(req.ModifiedRule.RuleEntity, req.ModifiedRule.RuleEntity.DayWorships, req.OktoikhDay, req, null);

                //обнуляем его, чтобы больше не участвовал в формировании
                req.ModifiedRule = null;
            }

            //получаем главное Правило и коллекцию богослужебных текстов
            (DayRule Rule, IEnumerable<DayWorship> Worships) almostReadyEntity = CalculatePriorities(req.ModifiedRule, req.MenologyRule, req.TriodionRule);

            //смотрим, не созданы ли уже настройки
            if (settings != null)
            {
                //созданы - значит был определен элемент для добавления
                settings.DayWorships.AddRange(almostReadyEntity.Worships);
            }

            
            TypiconRule seniorRule = almostReadyEntity.Rule;
            //смотрим, главным ставить само Правило или Знак службы
            /*
             * Если Правило имеет: 
             *      пустое RuleDefinition
             *      //и Правило помечено как Дополнение
             *      и определен Шаблон
             */
            if (
                string.IsNullOrEmpty(seniorRule.RuleDefinition)
                && seniorRule.IsAddition 
                && seniorRule.Template != null
                )
            {
                seniorRule = seniorRule.Template;
            }

            RuleHandlerSettings outputSettings = GetRecursiveSettings(seniorRule, almostReadyEntity.Worships, req.OktoikhDay, req, settings);

            return outputSettings;
        }

        /// <summary>
        /// Из трех правил выбирает главное и составляет коллекцию богослужебных текстов.
        /// Считаем, что ModifiedRule не является ДОПОЛНЕНИЕМ 
        /// </summary>
        /// <param name="modifiedRule"></param>
        /// <param name="menologyRule"></param>
        /// <param name="triodionRule"></param>
        /// <returns></returns>
        private (DayRule, IEnumerable<DayWorship>) CalculatePriorities(ModifiedRule modifiedRule, MenologyRule menologyRule, TriodionRule triodionRule)
        {
            MenologyRule menologyToCompare = null;
            TriodionRule tridionToCompare = null;

            //Приоритет Минеи
            int menologyPriority = int.MaxValue;
            if (modifiedRule?.RuleEntity is MenologyRule m)
            {
                menologyPriority = modifiedRule.Priority;
                menologyToCompare = m;
            }
            else
            {
                menologyPriority = menologyRule.Template.Priority;
                menologyToCompare = menologyRule;
            }

            //Приоритет Триоди
            int triodionPriority = int.MaxValue;
            if (modifiedRule?.RuleEntity is TriodionRule t)
            {
                triodionPriority = modifiedRule.Priority;
                tridionToCompare = t;
            }
            else
            {
                if (triodionRule != null)
                {
                    triodionPriority = triodionRule.Template.Priority;
                    tridionToCompare = triodionRule;
                }
            }

            List<DayRule> rulesList = new List<DayRule>();

            int result = menologyPriority - triodionPriority;
            //сравниваем
            switch (result)
            {
                case 1:
                case 0:
                    //senior Triodion, junior Menology
                    rulesList.Add(tridionToCompare);
                    rulesList.Add(menologyToCompare);
                    break;
                case -1:
                    //senior Menology, junior Triodion
                    rulesList.Add(menologyToCompare);
                    rulesList.Add(tridionToCompare);
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
                        rulesList.Add(tridionToCompare);
                    }
                    break;
            }

            //формируем список текстов
            List<DayWorship> dayWorships = new List<DayWorship>();
            rulesList.ForEach(c => dayWorships.AddRange(c.DayWorships));

            return (rulesList.First(), dayWorships);
        }

        private RuleHandlerSettings GetRecursiveSettings(TypiconRule rule, IEnumerable<DayWorship> dayServices, OktoikhDay oktoikhDay,
            GetRuleSettingsRequest req, RuleHandlerSettings additionalSettings)
        {
            RuleHandlerSettings outputSettings = Create(rule, dayServices, oktoikhDay, req, additionalSettings);

            /*
             * Если Правило имеет: 
             *      Правило помечено как Дополнение
             *      и определен Шаблон
             */

            if (/*string.IsNullOrEmpty(rule.RuleDefinition) && */rule.IsAddition && rule.Template != null)
            {
                //если правило определено и определено как добавление, входим в рекурсию
                outputSettings = GetRecursiveSettings(rule.Template, dayServices, oktoikhDay, req, outputSettings);
            }

            return outputSettings;
        }

        private RuleHandlerSettings Create(TypiconRule rule, IEnumerable<DayWorship> dayServices, OktoikhDay oktoikhDay, 
            GetRuleSettingsRequest req, RuleHandlerSettings additionalSettings)
        {
            return new RuleHandlerSettings()
            {
                Addition = additionalSettings,
                Date = req.Date,
                Rule = rule,
                DayWorships = dayServices.ToList(),
                OktoikhDay = oktoikhDay,
                Language = LanguageSettingsFactory.Create(req.Language),
                //ThrowExceptionIfInvalid = req.ThrowExceptionIfInvalid,
                ApplyParameters = req.ApplyParameters,
                CheckParameters = req.CheckParameters
            };
        }
    }
}
