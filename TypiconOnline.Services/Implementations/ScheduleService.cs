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

            #region  старое формирование имени
            //if (handlerRequest.Rules.Count > 1)
            //{
            //    for (int i = 1; i < handlerRequest.Rules.Count; i++)
            //    {
            //        scheduleDay.Name += handlerRequest.Rules[i].Name + " ";
            //    }
            //    //Если имеется короткое название, то будем добавлять только его
            //    if (handlerRequest.UseFullName && !string.IsNullOrEmpty(seniorTypiconRule.Name))//(string.IsNullOrEmpty(handlerRequest.ShortName))
            //    {
            //        scheduleDay.Name = (handlerRequest.PutSeniorRuleNameToEnd) ?
            //            scheduleDay.Name + seniorTypiconRule.Name :
            //            seniorTypiconRule.Name + " " + scheduleDay.Name;
            //    }
            //}
            //else if (handlerRequest.UseFullName)//(string.IsNullOrEmpty(handlerRequest.ShortName))
            //{
            //    scheduleDay.Name = seniorTypiconRule.Name;
            //}

            //if ((seniorTypiconRule is MenologyRule) && (inputRequest.Date.DayOfWeek == DayOfWeek.Sunday)
            //    && (seniorTypiconRule.Template.Priority > 1))
            //{
            //    //Если Триоди нет и воскресенье, находим название Недели из Октоиха
            //    //и добавляем название Недели в начало Name

            //    //Если имеется короткое название, то добавляем только его

            //    scheduleDay.Name = /*string.IsNullOrEmpty(handlerRequest.ShortName) ?
            //        BookStorage.Oktoikh.GetSundayName(inputRequest.Date) + " " + scheduleDay.Name :*/
            //        BookStorage.Oktoikh.GetSundayName(inputRequest.Date, handlerRequest.ShortName) + " " + scheduleDay.Name;

            //    //жестко задаем воскресный день
            //    //seniorTypiconRule.Template = inputRequest.TypiconEntity.TemplateSunday;
            //    handlerRequest.Rules.Insert(0, inputRequest.TypiconEntity.Settings.TemplateSunday);
            //    seniorTypiconRule = inputRequest.TypiconEntity.Settings.TemplateSunday;
            //}

            #endregion

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
            if (handlerRequest.DayServices.Count == 0) throw new ArgumentNullException("Ошибка: не определена коллекция богослужебных текстов.");

            string result = "";

            string language = inputRequest.TypiconEntity.Settings.DefaultLanguage;

            DayService seniorService = handlerRequest.DayServices[0];

            if (handlerRequest.DayServices.Count > 1)
            {
                for (int i = 1; i < handlerRequest.DayServices.Count; i++)
                {
                    result += handlerRequest.DayServices[i].ServiceName[language] + " ";
                }

                //Если имеется короткое название, то будем добавлять только его
                if (seniorService.UseFullName && !string.IsNullOrEmpty(seniorService.ServiceName[language]))//(string.IsNullOrEmpty(handlerRequest.ShortName))
                {
                    string n = seniorService.ServiceName[language];
                    result = (handlerRequest.PutSeniorRuleNameToEnd) ?
                        result + n :
                        n + " " + result;
                        //result + handlerRequest.Rule.Name :
                        //handlerRequest.Rule.Name + " " + result;
                }
            }
            else if (seniorService.UseFullName && handlerRequest.DayServices.Count == 1)//(string.IsNullOrEmpty(handlerRequest.ShortName))
            {
                result = seniorService.ServiceName[language];
            }

            if ((handlerRequest.Rule is MenologyRule) && (inputRequest.Date.DayOfWeek == DayOfWeek.Sunday)
                && (handlerRequest.Rule.Template.Priority > 1))
            {
                //Если Триоди нет и воскресенье, находим название Недели из Октоиха
                //и добавляем название Недели в начало Name

                //Если имеется короткое название, то добавляем только его

                result = /*string.IsNullOrEmpty(handlerRequest.ShortName) ?
                    BookStorage.Oktoikh.GetSundayName(inputRequest.Date) + " " + scheduleDay.Name :*/
                    BookStorage.Oktoikh.GetSundayName(inputRequest.Date, GetShortName(handlerRequest.DayServices, handlerRequest.Language)) + " " + result;

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
                result += dayServices[i].ServiceName[language];
                if (i < dayServices.Count - 1)
                {
                    result += ", ";
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
            RuleHandlerSettings outputSettings = null;

            //рассматриваем полученные измененные правила
            //и выбираем одно - с максимальным приоритетом
            if (modAbstractRules != null && modAbstractRules.Count > 0)
            {
                //выбираем измененное правило, максимальное по приоритету
                ModifiedRule abstrRule = modAbstractRules.Min();

                if (!abstrRule.AsAddition)
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
                    outputSettings = new RuleHandlerSettings()
                    {
                        Rule = abstrRule.RuleEntity,
                        //DayServices = abstrRule.RuleEntity.DayServices.co
                        Mode = inputRequest.Mode,
                        Language = inputRequest.Language,
                        CustomParameters = inputRequest.CustomParameters,
                    };
                }

                outputSettings.PutSeniorRuleNameToEnd = abstrRule.IsLastName;
                //outputRequest.ShortName = abstrRule.ShortName;
                //outputRequest.UseFullName = abstrRule.UseFullName;
            }

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

            //добавляем все измененные правила
            if (modAbstractRules != null)
            {
                modAbstractRules.Sort();

                for (int i = 0; i < modAbstractRules.Count; i++)
                {
                    ModifiedRule modRule = modAbstractRules[i];

                    outputSettings.DayServices.AddRange(modRule.RuleEntity.DayServices);
                    //задаем свойству Rule первое Правило из сортированного списка
                    if (i == 0)
                    {
                        outputSettings.Rule = modRule.RuleEntity;
                    }
                    
                }
            }

            return outputSettings;
        }

        /// <summary>
        /// Метод добавляет в modAbstractRules подложный ModifiedRule, содержащий typiconRule и его приоритет
        /// Испольуется для дальнейшей сортировки списка выходных правил метода ComposeRuleHandlerSettings
        /// </summary>
        /// <param name="modAbstractRules">список измененных правил</param>
        /// <param name="typiconRule"></param>
        private void AddFakeModRule(List<ModifiedRule> modAbstractRules, TypiconRule typiconRule)
        {
            if (modAbstractRules == null)
            {
                modAbstractRules = new List<ModifiedRule>();
            }

            ModifiedRule modRule = new ModifiedRule()
            {
                Priority = typiconRule.Template.Priority,
                RuleEntity = typiconRule as TriodionRule
            };

            modAbstractRules.Insert(0, modRule);

            //modAbstractRules.Add(modRule);
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
