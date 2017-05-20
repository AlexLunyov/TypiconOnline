using System;
using System.Collections.Generic;
using System.Linq;
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

            TypiconRule seniorTypiconRule = handlerRequest.Rules[0];

            //задаем имя дню

            if (handlerRequest.Rules.Count > 1)
            {
                for (int i = 1; i < handlerRequest.Rules.Count; i++)
                {
                    scheduleDay.Name += " " + handlerRequest.Rules[i].Name;
                }
                //Если имеется короткое название, то будем добавлять только его
                if (handlerRequest.UseFullName)//(string.IsNullOrEmpty(handlerRequest.ShortName))
                {
                    scheduleDay.Name = (handlerRequest.PutSeniorRuleNameToEnd) ?
                        scheduleDay.Name + " " + seniorTypiconRule.Name :
                        seniorTypiconRule.Name + " " + scheduleDay.Name;
                }
            }
            else if (handlerRequest.UseFullName)//(string.IsNullOrEmpty(handlerRequest.ShortName))
            {
                scheduleDay.Name = seniorTypiconRule.Name;
            }

            if ((seniorTypiconRule is MenologyRule) && (inputRequest.Date.DayOfWeek == DayOfWeek.Sunday))
            {
                //Если Триоди нет и воскресенье, находим название Недели из Октоиха
                //и добавляем название Недели в начало Name

                //Если имеется короткое название, то добавляем только его

                scheduleDay.Name = /*string.IsNullOrEmpty(handlerRequest.ShortName) ?
                    BookStorage.Oktoikh.GetSundayName(inputRequest.Date) + " " + scheduleDay.Name :*/
                    BookStorage.Oktoikh.GetSundayName(inputRequest.Date, handlerRequest.ShortName) + " " + scheduleDay.Name;

                //жестко задаем воскресный день
                //seniorTypiconRule.Template = inputRequest.TypiconEntity.TemplateSunday;
                handlerRequest.Rules.Insert(0, inputRequest.TypiconEntity.TemplateSunday);
                seniorTypiconRule = inputRequest.TypiconEntity.TemplateSunday;
            }

            scheduleDay.Date = inputRequest.Date;

            //наполняем
            seniorTypiconRule.Rule.Interpret(inputRequest.Date, inputRequest.RuleHandler);

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

                seniorTypiconRule = handlerRequest.Rules[0];

                if ((seniorTypiconRule is MenologyRule) &&
                    (inputRequest.Date.DayOfWeek == DayOfWeek.Sunday))
                {
                    //если нет Триоди и воскресенье
                    //жестко задаем воскресный день
                    //seniorTypiconRule.Template = inputRequest.TypiconEntity.TemplateSunday;

                    handlerRequest.Rules.Insert(0, inputRequest.TypiconEntity.TemplateSunday);
                    seniorTypiconRule = inputRequest.TypiconEntity.TemplateSunday;
                }

                inputRequest.RuleHandler.Initialize(handlerRequest);

                //наполняем
                seniorTypiconRule.Rule.Interpret(inputRequest.Date, inputRequest.RuleHandler);

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
        /// 1.	Находим связанные правила для введенной даты
        /// 2.	Делаем фильтрацию
        /// 3.	Из оставшихся выбираем главную службу и второстепенную
        /// </summary>
        /// <param name="inputRequest"></param>
        /// <returns></returns>
        private RuleHandlerRequest ComposeRuleHandlerRequest(GetScheduleDayRequest inputRequest)
        {
            //находим MenologyRule

            MenologyRule menologyRule = inputRequest.TypiconEntity.GetMenologyRule(inputRequest.Date);

            if (menologyRule == null)
                throw new ArgumentNullException("MenologyRule");

            //находим TriodionRule

            DateTime easterDate = EasterStorage.Instance.GetCurrentEaster(inputRequest.Date.Year);

            int daysFromEaster = inputRequest.Date.Subtract(easterDate).Days;

            TriodionRule triodionRule = inputRequest.TypiconEntity.GetTriodionRule(daysFromEaster);

            //находим ModifiedRule

            ModifiedMenologyRule modMenologyRule = null;
            ModifiedTriodionRule modTriodionRule = null;

            List<ModifiedRule> modAbstractRules = inputRequest.TypiconEntity.GetModifiedRules(inputRequest.Date);

            //создаем выходной объект
            RuleHandlerRequest outputRequest = new RuleHandlerRequest() { Mode = inputRequest.Mode };

            //рассматриваем полученные измененные правила
            if (modAbstractRules != null && modAbstractRules.Count > 0)
            {
                bool isNotAddition = modAbstractRules.TrueForAll(c => !c.AsAddition);
                bool isLastName = !modAbstractRules.TrueForAll(c => !c.IsLastName);

                //сортируем по приоитету, если измененных правил больше одного
                //if (modAbstractRules.Count > 1)
                //{
                //    modAbstractRules.Sort(delegate (ModifiedRule x, ModifiedRule y)
                //    {
                //        return x.Priority.CompareTo(y.Priority);
                //    });
                //}

                //выбираем измененное правило, максимальное по приоритету
                ModifiedRule abstrRule = modAbstractRules.Min();

                //считаем, что в списке правила только Минейные или Триодные

                if (abstrRule is ModifiedMenologyRule)
                {
                    if (isNotAddition)
                    {
                        modMenologyRule = abstrRule as ModifiedMenologyRule;
                    }
                }

                if (abstrRule is ModifiedTriodionRule)
                {
                    if (isNotAddition)
                    {
                        modTriodionRule = abstrRule as ModifiedTriodionRule;
                    }
                }

                outputRequest.PutSeniorRuleNameToEnd = isLastName;
                outputRequest.ShortName = abstrRule.ShortName;
                outputRequest.UseFullName = abstrRule.UseFullName;
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
                        //outputRequest.Rules.Add(triodionRule);

                        AddFakeModRule(modAbstractRules, triodionRule);
                    }
                    if (modMenologyRule == null)
                    {
                        //outputRequest.Rules.Add(menologyRule);

                        AddFakeModRule(modAbstractRules, menologyRule);
                    }
                    break;
                case -1:
                    //senior Menology, junior Triodion
                    if (modMenologyRule == null)
                    {
                        //outputRequest.Rules.Add(menologyRule);

                        AddFakeModRule(modAbstractRules, menologyRule);
                    }
                    if (modTriodionRule == null)
                    {
                        //outputRequest.Rules.Add(triodionRule);

                        AddFakeModRule(modAbstractRules, triodionRule);
                    }
                    break;
                default:
                    if (result < -1)
                    {
                        //только Минея
                        if (modMenologyRule == null)
                        {
                            //outputRequest.Rules.Add(menologyRule);

                            AddFakeModRule(modAbstractRules, menologyRule);
                        }
                    }
                    else
                    {
                        //только Триодь
                        if (modTriodionRule == null)
                        {
                            //outputRequest.Rules.Add(triodionRule);

                            AddFakeModRule(modAbstractRules, triodionRule);
                        }
                    }
                    break;
            }

            //добавляем все измененные правила
            if (modAbstractRules != null)
            {
                modAbstractRules.Sort();

                modAbstractRules.ForEach(c =>
                {
                    if (c is ModifiedTriodionRule)
                    {
                        outputRequest.Rules.Add((c as ModifiedTriodionRule).RuleEntity);
                    }
                    else
                    {
                        outputRequest.Rules.Add((c as ModifiedMenologyRule).RuleEntity);
                    }
                });
            }

            //сортируем по приоитету, если правил больше одного

            //TODO: это бессмысленно, потому как приоритет измененных правил потерялся
            //if (outputRequest.Rules.Count > 1)
            //{
            //    outputRequest.Rules.Sort(delegate (TypiconRule x, TypiconRule y)
            //    {
            //        return x.Template.Priority.CompareTo(y.Template.Priority);
            //    });
            //}

            return outputRequest;
        }

        /// <summary>
        /// Метод добавляет в modAbstractRules подложный ModifiedRule, содержащий typiconRule и его приоритет
        /// Испольуется для дальнейшей сортировки списка выходных правил метода ComposeRuleHandlerRequest
        /// </summary>
        /// <param name="modAbstractRules">список измененных правил</param>
        /// <param name="typiconRule"></param>
        private void AddFakeModRule(List<ModifiedRule> modAbstractRules, TypiconRule typiconRule)
        {
            if (modAbstractRules == null)
            {
                modAbstractRules = new List<ModifiedRule>();
            }

            ModifiedRule modRule = null;

            if (typiconRule is TriodionRule)
            {
                modRule = new ModifiedTriodionRule()
                {
                    Priority = typiconRule.Template.Priority,
                    RuleEntity = typiconRule as TriodionRule
                };
            }
            else
            {
                modRule = new ModifiedMenologyRule()
                {
                    Priority = typiconRule.Template.Priority,
                    RuleEntity = typiconRule as MenologyRule
                };
            }

            modAbstractRules.Add(modRule);
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
