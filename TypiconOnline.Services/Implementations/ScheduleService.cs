using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.AppServices.Services;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Services
{
    public class ScheduleService : IScheduleService
    {
        public GetScheduleDayResponse GetScheduleDay(GetScheduleDayRequest inputRequest)
        {
            //смотрим, есть ли ModifiedRules с таким годом, как у date
            //формируем этот список
            //на основании MenologyRule, TriodionRule и ModifiedRule (если имеется таковой) 
            //возвращаем ScheduleHandled Day

            if (inputRequest.TypiconEntity == null)
                throw new ArgumentNullException("TypiconEntity");

            if (inputRequest.Handler == null)
                throw new ArgumentNullException("RuleHandler");

            HandlingMode inputMode = inputRequest.Mode;

            if (inputMode == HandlingMode.AstronimicDay)
            {
                inputRequest.Mode = HandlingMode.ThisDay;
            }

            //Формируем данные для обработки
            RuleHandlerSettings handlerSettings = ComposeRuleHandlerSettings(inputRequest);

            inputRequest.Handler.Settings = handlerSettings;

            ScheduleDay scheduleDay = new ScheduleDay();

            //задаем имя дню
            scheduleDay.Name = ComposeServiceName(inputRequest, handlerSettings);

            scheduleDay.Date = inputRequest.Date;

            scheduleDay.Sign = (handlerSettings.Rule is Sign) ? (handlerSettings.Rule as Sign).Number : GetTemplateSignID(handlerSettings.Rule.Template);

            if (inputRequest.ConvertSignToHtmlBinding)
            {
                scheduleDay.Sign = SignMigrator.GetOldId(k => k.Value.NewID == scheduleDay.Sign);
            }

            //наполняем
            handlerSettings.Rule.Rule.Interpret(inputRequest.Date, inputRequest.Handler);

            ContainerViewModel container = inputRequest.Handler.GetResult();

            if (container != null)
            {
                scheduleDay.Schedule.ChildElements.AddRange(container.ChildElements);
            }

            if (inputMode == HandlingMode.AstronimicDay)
            {
                //ищем службы следующего дня с маркером IsDayBefore == true

                inputRequest.Date = inputRequest.Date.AddDays(1);
                inputRequest.Mode = HandlingMode.DayBefore;

                handlerSettings = ComposeRuleHandlerSettings(inputRequest);

                if ((handlerSettings.Rule is MenologyRule) &&
                    (inputRequest.Date.DayOfWeek == DayOfWeek.Sunday))
                {
                    //если нет Триоди и воскресенье
                    //жестко задаем воскресный день
                    handlerSettings.Rule = inputRequest.TypiconEntity.Settings.TemplateSunday;
                }

                inputRequest.Handler.Settings = handlerSettings;

                //наполняем
                handlerSettings.Rule.Rule.Interpret(inputRequest.Date, inputRequest.Handler);

                container = inputRequest.Handler.GetResult();

                if (container != null)
                {
                    scheduleDay.Schedule.ChildElements.AddRange(container.ChildElements);
                }

                //возвращаем назад изменение даты
                inputRequest.Date = inputRequest.Date.AddDays(-1);
                
            }

            //возвращаем назад
            inputRequest.Mode = inputMode;

            GetScheduleDayResponse response = new GetScheduleDayResponse()
            {
                Day = scheduleDay
            };

            return response;
        }

        private string ComposeServiceName(GetScheduleDayRequest inputRequest, RuleHandlerSettings handlerRequest)
        {
            //находим самое последнее правило - добавление
            while (handlerRequest.Addition != null)
            {
                handlerRequest = handlerRequest.Addition;
            }

            string result = "";

            string language = inputRequest.TypiconEntity.Settings.DefaultLanguage;

            DayService seniorService = handlerRequest.DayServices[0];

            //собираем все имена текстов, кроме главного
            if (handlerRequest.DayServices.Count > 1)
            {
                for (int i = 1; i < handlerRequest.DayServices.Count; i++)
                {
                    result += handlerRequest.DayServices[i].ServiceName[language] + " ";
                }
            }

            //а теперь разбираемся с главным

            string s = seniorService.ServiceName[language];

            if (inputRequest.Date.DayOfWeek != DayOfWeek.Sunday
                || (inputRequest.Date.DayOfWeek == DayOfWeek.Sunday 
                    && (seniorService.UseFullName || seniorService.ServiceShortName.IsTextEmpty)))
            {
                result = (handlerRequest.PutSeniorRuleNameToEnd) ?
                        result + s :
                        s + " " + result;
            }

            if ((handlerRequest.Rule is MenologyRule) 
                && (inputRequest.Date.DayOfWeek == DayOfWeek.Sunday)
                && (handlerRequest.Rule.Template.Priority > 1))
            {
                //Если Триоди нет и воскресенье, находим название Недели из Октоиха
                //и добавляем название Недели в начало Name

                //Если имеется короткое название, то добавляем только его

                result = BookStorage.Oktoikh.GetSundayName(inputRequest.Date, 
                    GetShortName(handlerRequest.DayServices, handlerRequest.Language)) + " " + result;

                //жестко задаем воскресный день
                handlerRequest.Rule = inputRequest.TypiconEntity.Settings.TemplateSunday;
            }

            return result;
        }

        private string GetShortName(List<DayService> dayServices, string language)
        {
            string result = "";

            for (int i = 0; i < dayServices.Count; i++)
            {
                string s = dayServices[i].ServiceShortName[language];

                if (!string.IsNullOrEmpty(s))
                {
                    result = (!string.IsNullOrEmpty(result)) ? result + ", " + s : s;
                }
            }

            return result;
        }

        /// <summary>
        /// Формирует запрос для дальнейшей обработки: главную и второстепенную службу, HandlingMode
        /// Процесс описан в документации
        /// </summary>
        /// <param name="inputRequest"></param>
        /// <returns></returns>
        private RuleHandlerSettings ComposeRuleHandlerSettings(GetScheduleDayRequest inputRequest)
        {
            //находим MenologyRule

            MenologyRule menologyRule = inputRequest.TypiconEntity.GetMenologyRule(inputRequest.Date);

            if (menologyRule == null)
                throw new ArgumentNullException("MenologyRule");

            //находим TriodionRule

            TriodionRule triodionRule = inputRequest.TypiconEntity.GetTriodionRule(inputRequest.Date);

            //находим ModifiedRule

            ModifiedRule modMenologyRule = null;
            ModifiedRule modTriodionRule = null;

            List<ModifiedRule> modAbstractRules = inputRequest.TypiconEntity.GetModifiedRules(inputRequest.Date);

            //создаем выходной объект
            RuleHandlerSettings additionalSettings = null;

            //рассматриваем полученные измененные правила
            //и выбираем одно - с максимальным приоритетом
            if (modAbstractRules?.Count > 0)
            {
                //выбираем измененное правило, максимальное по приоритету
                ModifiedRule abstrRule = modAbstractRules.Min();

                if (!abstrRule.IsAddition)
                {
                    if (abstrRule.RuleEntity is MenologyRule)
                    {
                        modMenologyRule = abstrRule;
                    }

                    if (abstrRule.RuleEntity is TriodionRule)
                    {
                        modTriodionRule = abstrRule;
                    }
                }
                else
                {
                    //создаем первый объект, который в дальнейшем станет ссылкой Addition у выбранного правила
                    additionalSettings = new RuleHandlerSettings()
                    {
                        Rule = abstrRule.RuleEntity,
                        DayServices = abstrRule.RuleEntity.DayServices.ToList(),
                        Mode = inputRequest.Mode,
                        Language = inputRequest.Language,
                        CustomParameters = inputRequest.CustomParameters,
                        PutSeniorRuleNameToEnd = abstrRule.IsLastName
                    };

                    modAbstractRules.Clear();
                }
            }

            #region Вычисление приоритетов и наполнение списка modAbstractRules

            //определяем приоритет и находим, какие объекты будем обрабатывать

            int menologyPriority = (modMenologyRule != null) ? modMenologyRule.Priority : menologyRule.Template.Priority;

            int triodionPriority = int.MaxValue;

            //по умолчанию задаем выходному значению Триодь
            if (modTriodionRule != null)
            {
                triodionPriority = modTriodionRule.Priority;
            }
            else
            {
                if (triodionRule != null)
                {
                    triodionPriority = triodionRule.Template.Priority;
                }
            }

            int result = menologyPriority - triodionPriority;

            switch (result)
            {
                case 1:
                case 0:
                    //senior Triodion, junior Menology
                    if (modTriodionRule == null)
                    {
                        AddFakeModRule(modAbstractRules, triodionRule);
                    }
                    if (modMenologyRule == null)
                    {
                        AddFakeModRule(modAbstractRules, menologyRule);
                    }
                    break;
                case -1:
                    //senior Menology, junior Triodion
                    if (modMenologyRule == null)
                    {
                        AddFakeModRule(modAbstractRules, menologyRule);
                    }
                    if (modTriodionRule == null)
                    {
                        AddFakeModRule(modAbstractRules, triodionRule);
                    }
                    break;
                default:
                    if (result < -1)
                    {
                        //только Минея
                        if (modMenologyRule == null)
                        {
                            AddFakeModRule(modAbstractRules, menologyRule);
                        }
                    }
                    else
                    {
                        //только Триодь
                        if (modTriodionRule == null)
                        {
                            AddFakeModRule(modAbstractRules, triodionRule);
                        }
                    }
                    break;
            }

            #endregion

            //сортируем полученный список по приоритетам
            modAbstractRules.Sort();

            //получаем коллекцию богослужебных текстов
            List<DayService> dayServices = GetDayServices(modAbstractRules);

            //смотрим, не созданы ли уже настройки
            if (additionalSettings != null)
            {
                //созданы - значит был определен элемент для добавления
                additionalSettings.DayServices.AddRange(dayServices);
            }

            RuleHandlerSettings outputSettings = GetRecursiveSettings(modAbstractRules[0].RuleEntity, dayServices, inputRequest, additionalSettings);

            return outputSettings;
        }

        private RuleHandlerSettings GetRecursiveSettings(TypiconRule rule, List<DayService> dayServices, GetScheduleDayRequest inputRequest,
            RuleHandlerSettings additionalSettings)
        {
            RuleHandlerSettings outputSettings = new RuleHandlerSettings()
            {
                Addition = additionalSettings,
                Rule = rule,
                DayServices = dayServices.ToList(),
                Mode = inputRequest.Mode,
                Language = inputRequest.Language,
                CustomParameters = inputRequest.CustomParameters
            };

            
            if (!string.IsNullOrEmpty(rule.RuleDefinition) && rule.IsAddition && rule.Template != null)
            {
                //если правило определено и определено как добавление входим в рекурсию
                outputSettings = GetRecursiveSettings(rule.Template, dayServices, inputRequest, outputSettings);
            }

            return outputSettings;
        }

        private List<DayService> GetDayServices(List<ModifiedRule> modRules)
        {
            List<DayService> result = new List<DayService>();

            modRules.ForEach(c => result.AddRange(c.RuleEntity.DayServices));

            return result;
        }

        /// <summary>
        /// Метод добавляет в modAbstractRules подложный ModifiedRule, содержащий typiconRule и его приоритет
        /// Испольуется для дальнейшей сортировки списка выходных правил метода ComposeRuleHandlerSettings
        /// </summary>
        /// <param name="modAbstractRules">список измененных правил</param>
        /// <param name="typiconRule"></param>
        private void AddFakeModRule(List<ModifiedRule> modAbstractRules, DayRule typiconRule)
        {
            if (modAbstractRules == null)
            {
                modAbstractRules = new List<ModifiedRule>();
            }

            ModifiedRule modRule = new ModifiedRule()
            {
                Priority = typiconRule.Template.Priority,
                RuleEntity = typiconRule
            };

            modAbstractRules.Insert(0, modRule);
        }

        /// <summary>
        /// Возвращает ID предустановленного шаблона.
        /// </summary>
        /// <param name="sign">Вводимый ID для проверки</param>
        /// <returns></returns>
        private int GetTemplateSignID(Sign sign)
        {
            return (sign.Template == null) ? sign.Number : GetTemplateSignID(sign.Template);
        }

        public GetScheduleWeekResponse GetScheduleWeek(GetScheduleWeekRequest request)
        {
            ScheduleWeek week = new ScheduleWeek() 
            {
                Name = BookStorage.Oktoikh.GetWeekName(request.Date, false)
            };

            GetScheduleDayRequest dayRequest = new GetScheduleDayRequest()
            {
                Date = request.Date,
                Mode = request.Mode,
                Handler = request.Handler,
                TypiconEntity = request.TypiconEntity,
                Language = request.Language,
                CustomParameters = request.CustomParameters,
            };

            int i = 0;

            while (i < 7)
            {
                GetScheduleDayResponse dayResponse = GetScheduleDay(dayRequest);
                week.Days.Add(dayResponse.Day);
                dayRequest.Date = dayRequest.Date.AddDays(1);
                i++;
            }

            return new GetScheduleWeekResponse() { Week = week };
        }
    }
}
