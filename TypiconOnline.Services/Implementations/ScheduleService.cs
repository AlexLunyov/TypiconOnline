using System;
using System.Collections.Generic;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.AppServices.Services;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Easter;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

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

            if (inputRequest.RuleHandler == null)
                throw new ArgumentNullException("RuleHandler");

            HandlingMode inputMode = inputRequest.Mode;

            if (inputMode == HandlingMode.AstronimicDay)
            {
                inputRequest.Mode = HandlingMode.ThisDay;
            }

            //Формируем данные для обработки
            RuleHandlerRequest handlerRequest = ComposeRuleHandlerRequest(inputRequest);

            inputRequest.RuleHandler.Initialize(handlerRequest);

            ScheduleDay scheduleDay = new ScheduleDay();

            //задаем имя дню
            if (handlerRequest.SeniorTypiconRule is MenologyRule)
            {
                scheduleDay.Name = (handlerRequest.SeniorTypiconRule as MenologyRule).Day.Name;

                if (handlerRequest.AdditionModifiedRule != null)
                {
                    //добавляем в начало ModifiedRule, помеченное как AsAddition

                    scheduleDay.Name = (handlerRequest.AdditionModifiedRule as ModifiedMenologyRule).RuleEntity.Day.Name 
                        + scheduleDay.Name;
                }

                if (handlerRequest.JuniorTypiconRule != null)
                {
                    //если в измененном дне указано, чтобы ставить в конец  - исполняем
                    scheduleDay.Name = (handlerRequest.PutSeniorRuleNameToEnd) ?
                        (handlerRequest.JuniorTypiconRule as TriodionRule).Day.Name + " " + scheduleDay.Name :
                        scheduleDay.Name + " " + (handlerRequest.JuniorTypiconRule as TriodionRule).Day.Name;
                }
                else if (inputRequest.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    //Если Триоди нет и воскресенье, находим название Недели из Октоиха
                    //и добавляем название Недели в начало Name

                    //Если имеется короткое название, то добавляем только его

                    scheduleDay.Name = string.IsNullOrEmpty(handlerRequest.ShortName) ?
                        BookStorage.Oktoikh.GetSundayName(inputRequest.Date) + " " + scheduleDay.Name :
                        BookStorage.Oktoikh.GetSundayName(inputRequest.Date, handlerRequest.ShortName);

                    //жестко задаем воскресный день
                    handlerRequest.SeniorTypiconRule.Template = inputRequest.TypiconEntity.TemplateSunday;
                }
            }

            if (handlerRequest.SeniorTypiconRule is TriodionRule)
            {
                scheduleDay.Name = (handlerRequest.SeniorTypiconRule as TriodionRule).Day.Name;

                if (handlerRequest.JuniorTypiconRule != null)
                {
                    scheduleDay.Name += " " + (handlerRequest.JuniorTypiconRule as MenologyRule).Day.Name;
                }
            }

            scheduleDay.Date = inputRequest.Date;

            

            //наполняем
            handlerRequest.SeniorTypiconRule.Rule.Interpret(inputRequest.Date, inputRequest.RuleHandler);

            RuleContainer container = inputRequest.RuleHandler.GetResult();

            if (container != null)
            {
                scheduleDay.Schedule.ChildElements.AddRange(container.ChildElements);
            }


            if (inputMode == HandlingMode.AstronimicDay)
            {
                //ищем службы следующего дня с маркером IsDayBefore == true

                inputRequest.Date = inputRequest.Date.AddDays(1);
                inputRequest.Mode = HandlingMode.DayBefore;

                handlerRequest = ComposeRuleHandlerRequest(inputRequest);

                if ((handlerRequest.SeniorTypiconRule is MenologyRule) &&
                    (handlerRequest.JuniorTypiconRule == null) &&
                    (inputRequest.Date.DayOfWeek == DayOfWeek.Sunday))
                {
                    //если нет Триоди и воскресенье
                    //жестко задаем воскресный день
                    handlerRequest.SeniorTypiconRule.Template = inputRequest.TypiconEntity.TemplateSunday;
                }

                inputRequest.RuleHandler.Initialize(handlerRequest);

                //наполняем
                handlerRequest.SeniorTypiconRule.Rule.Interpret(inputRequest.Date, inputRequest.RuleHandler);

                container = inputRequest.RuleHandler.GetResult();

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

        /// <summary>
        /// Формирует запрос для дальнейшей обработки: главную и второстепенную службу, HandlingMode
        /// </summary>
        /// <param name="inputRequest"></param>
        /// <returns></returns>
        private RuleHandlerRequest ComposeRuleHandlerRequest(GetScheduleDayRequest inputRequest)
        {
            //находим MenologyRule

            //MenologyRule menologyRule = (MenologyRule)inputRequest.TypiconEntity.RulesFolder.
            //    FindRule(c => ((c is MenologyRule) && ((c as MenologyRule).Day.GetCurrentDate(inputRequest.Date.Year) == inputRequest.Date)));

            //MenologyRule menologyRule = inputRequest.TypiconEntity.RulesFolder.FindMenologyRule(inputRequest.Date);

            MenologyRule menologyRule = inputRequest.TypiconEntity.GetMenologyRule(inputRequest.Date);

            if (menologyRule == null)
                throw new ArgumentNullException("MenologyRule");

            //находим TriodionRule

            DateTime easterDate = EasterStorage.Instance.GetCurrentEaster(inputRequest.Date.Year);

            int daysFromEaster = inputRequest.Date.Subtract(easterDate).Days;

            //TriodionRule triodionRule = (TriodionRule)inputRequest.TypiconEntity.RulesFolder.
            //    FindRule(c => ((c is TriodionRule) && ((c as TriodionRule).Day.DaysFromEaster == daysFromEaster)));

            //TriodionRule triodionRule = inputRequest.TypiconEntity.RulesFolder.FindTriodionRule(daysFromEaster);

            TriodionRule triodionRule = inputRequest.TypiconEntity.GetTriodionRule(daysFromEaster);

            //находим ModifiedRule

            ModifiedMenologyRule modMenologyRule = null;
            ModifiedTriodionRule modTriodionRule = null;

            ModifiedRule modAbstractRule = inputRequest.TypiconEntity.GetModifiedRule(inputRequest.Date);

            //создаем выходной объект
            RuleHandlerRequest outputRequest = new RuleHandlerRequest() { Mode = inputRequest.Mode };

            if (modAbstractRule != null)
            {
                outputRequest.PutSeniorRuleNameToEnd = modAbstractRule.IsLastName;
                outputRequest.ShortName = modAbstractRule.ShortName;
            }

            if ((modAbstractRule != null) && (modAbstractRule is ModifiedMenologyRule))
                modMenologyRule = modAbstractRule as ModifiedMenologyRule;

            if ((modAbstractRule != null) && (modAbstractRule is ModifiedTriodionRule))
                modTriodionRule = modAbstractRule as ModifiedTriodionRule;


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
                    outputRequest.SeniorTypiconRule = (modTriodionRule != null) ? modTriodionRule.RuleEntity : triodionRule;
                    outputRequest.JuniorTypiconRule = (modMenologyRule != null) ? modMenologyRule.RuleEntity : menologyRule;
                    break;
                case -1:
                    //senior Menology, junior Triodion
                    outputRequest.SeniorTypiconRule = (modMenologyRule != null) ? modMenologyRule.RuleEntity : menologyRule; 
                    outputRequest.JuniorTypiconRule = (modTriodionRule != null) ? modTriodionRule.RuleEntity : triodionRule;
                    break;
                default:
                    if (result < -1)
                    {
                        //только Минея
                        if (modMenologyRule != null)
                        {
                            if (modMenologyRule.AsAddition)
                            {
                                outputRequest.SeniorTypiconRule = modMenologyRule.ru;
                                outputRequest.JuniorTypiconRule = menologyRule;
                            }
                            else
                            {
                                outputRequest.SeniorTypiconRule = modMenologyRule.RuleEntity;
                            }
                        }
                        else
                        {
                            outputRequest.SeniorTypiconRule = menologyRule;
                        }
                        //outputRequest.SeniorTypiconRule = ((modMenologyRule != null) && (!modMenologyRule.AsAddition)) 
                        //    ? modMenologyRule.RuleEntity : menologyRule;
                        outputRequest.JuniorTypiconRule = null;
                    }
                    else
                    {
                        //только Триодь
                        outputRequest.SeniorTypiconRule = (modTriodionRule != null) ? modTriodionRule.RuleEntity : triodionRule;
                        outputRequest.JuniorTypiconRule = null;
                    }
                    break;
            }

            return outputRequest;
        }

        public GetScheduleWeekResponse GetScheduleWeek(GetScheduleWeekRequest request)
        {
            List<ScheduleDay> week = new List<ScheduleDay>();

            GetScheduleDayRequest dayRequest = new GetScheduleDayRequest()
            {
                Date = request.Date,
                Mode = request.Mode,
                RuleHandler = request.RuleHandler,
                TypiconEntity = request.TypiconEntity,
                CustomParameters = request.CustomParameters
            };

            int i = 0;

            while (i < 7)
            {
                GetScheduleDayResponse dayResponse = GetScheduleDay(dayRequest);
                week.Add(dayResponse.Day);
                dayRequest.Date = dayRequest.Date.AddDays(1);
                i++;
            }

            return new GetScheduleWeekResponse() { Days = week };
        }
    }
}
