using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.AppServices.Services;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Easter;
using TypiconOnline.Domain.Rendering;
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

            RenderContainer container = inputRequest.Handler.GetResult();

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
            string result = "";

            string language = inputRequest.TypiconEntity.Settings.DefaultLanguage;

            if (handlerRequest.DayServices.Count > 1)
            {
                for (int i = 1; i < handlerRequest.DayServices.Count; i++)
                {
                    result += handlerRequest.DayServices[i].ServiceName.Text[language] + " ";
                }
                //Если имеется короткое название, то будем добавлять только его
                if (handlerRequest.UseFullName && !string.IsNullOrEmpty(handlerRequest.Rule.Name))//(string.IsNullOrEmpty(handlerRequest.ShortName))
                {
                    string n = handlerRequest.DayServices[0].ServiceName.Text[language];
                    result = (handlerRequest.PutSeniorRuleNameToEnd) ?
                        result + n :
                        n + " " + result;
                        //result + handlerRequest.Rule.Name :
                        //handlerRequest.Rule.Name + " " + result;
                }
            }
            else if (handlerRequest.UseFullName && handlerRequest.DayServices.Count == 1)//(string.IsNullOrEmpty(handlerRequest.ShortName))
            {
                result = handlerRequest.DayServices[0].ServiceName.Text[language];
            }

            if ((handlerRequest.Rule is MenologyRule) && (inputRequest.Date.DayOfWeek == DayOfWeek.Sunday)
                && (handlerRequest.Rule.Template.Priority > 1))
            {
                //Если Триоди нет и воскресенье, находим название Недели из Октоиха
                //и добавляем название Недели в начало Name

                //Если имеется короткое название, то добавляем только его

                result = /*string.IsNullOrEmpty(handlerRequest.ShortName) ?
                    BookStorage.Oktoikh.GetSundayName(inputRequest.Date) + " " + scheduleDay.Name :*/
                    BookStorage.Oktoikh.GetSundayName(inputRequest.Date, handlerRequest.ShortName) + " " + result;

                //жестко задаем воскресный день
                handlerRequest.Rule = inputRequest.TypiconEntity.Settings.TemplateSunday;
            }

            return result;
        }

        /// <summary>
        /// Формирует запрос для дальнейшей обработки: главную и второстепенную службу, HandlingMode
        /// 1.	Находим связанные правила для введенной даты
        /// 2.	Делаем фильтрацию
        /// 3.	Из оставшихся выбираем главную службу и второстепенную
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

            //DateTime easterDate = EasterStorage.Instance.GetCurrentEaster(inputRequest.Date.Year);

            //int daysFromEaster = inputRequest.Date.Subtract(easterDate).Days;

            TriodionRule triodionRule = inputRequest.TypiconEntity.GetTriodionRule(inputRequest.Date);

            //находим ModifiedRule

            ModifiedMenologyRule modMenologyRule = null;
            ModifiedTriodionRule modTriodionRule = null;

            List<ModifiedRule> modAbstractRules = inputRequest.TypiconEntity.GetModifiedRules(inputRequest.Date);

            //создаем выходной объект
            RuleHandlerSettings outputRequest = new RuleHandlerSettings()
            {
                Mode = inputRequest.Mode,
                Language = inputRequest.Handler.Settings.Language
            };

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

                for (int i = 0; i < modAbstractRules.Count; i++)
                {
                    ModifiedRule modRule = modAbstractRules[i];
                    if (modRule is ModifiedTriodionRule)
                    {
                        outputRequest.DayServices.AddRange((modRule as ModifiedTriodionRule).RuleEntity.DayServices);
                        //задаем свойству Rule первое Правило из сортированного списка
                        if (i == 0)
                        {
                            outputRequest.Rule = (modRule as ModifiedTriodionRule).RuleEntity;
                        }
                    }
                    else
                    {
                        outputRequest.DayServices.AddRange((modRule as ModifiedMenologyRule).RuleEntity.DayServices);
                        //задаем свойству Rule первое Правило из сортированного списка
                        if (i == 0)
                        {
                            outputRequest.Rule = (modRule as ModifiedMenologyRule).RuleEntity;
                        }
                    }
                }
            }

            return outputRequest;
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
