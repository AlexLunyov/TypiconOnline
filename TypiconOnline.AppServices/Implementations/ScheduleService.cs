using System;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Служба для вычисления Выходных форм 
    /// </summary>
    public class ScheduleService : IScheduleService
    {
        IScheduleDataCalculator dataCalculator;
        IScheduleDayNameComposer nameComposer;

        public ScheduleService(IScheduleDataCalculator dataCalculator, IScheduleDayNameComposer nameComposer)
        {
            this.dataCalculator = dataCalculator ?? throw new ArgumentNullException("dataCalculator");
            this.nameComposer = nameComposer ?? throw new ArgumentNullException("nameComposer");
        }

        public GetScheduleDayResponse GetScheduleDay(GetScheduleDayRequest request)
        {
            //находим метод обработки дня
            HandlingMode mode = request.CheckParameters.GetMode();
            
            //Формируем данные для обработки
            var settingsRequest = new ScheduleDataCalculatorRequest()
            {
                Date = request.Date,
                TypiconVersionId = request.TypiconId,
                ApplyParameters = request.ApplyParameters,
                CheckParameters = request.CheckParameters
                    .SetModeParam((mode == HandlingMode.AstronomicDay) ? HandlingMode.ThisDay : mode)
            };

            OutputDay scheduleDay = GetOrFillScheduleDay(settingsRequest, request.Handler);

            if (mode == HandlingMode.AstronomicDay)
            {
                //ищем службы следующего дня с маркером IsDayBefore == true
                settingsRequest.Date = request.Date.AddDays(1);
                settingsRequest.CheckParameters = settingsRequest.CheckParameters.SetModeParam(HandlingMode.DayBefore);

                scheduleDay = GetOrFillScheduleDay(settingsRequest, request.Handler, scheduleDay);
            }

            return new GetScheduleDayResponse()
            {
                Day = scheduleDay
            };
        }

        

        private OutputDay GetOrFillScheduleDay(ScheduleDataCalculatorRequest request, ScheduleHandler handler, OutputDay scheduleDay = null)
        {
            //Формируем данные для обработки
            var response = dataCalculator.Calculate(request);

            var settings = response.Settings;

            handler.Settings = settings;

            settings.RuleContainer.Interpret(handler);

            var container = handler.GetResult();

            if (scheduleDay == null)
            {
                //Sign sign = (settings.Rule is Sign s) ? s : GetTemplateSign(settings.Rule.Template);
                var sign = GetPredefinedTemplate(response.Rule.Template);

                //Если settings.SignNumber определен в ModifiedRule, то назначаем его
                int signNumber = settings.SignNumber ?? sign.Number.Value;

                scheduleDay = new OutputDay
                {
                    //задаем имя дню
                    Name = nameComposer.Compose(request.Date, response.Rule.Template.Priority, settings.AllWorships),
                    Date = request.Date,
                    SignNumber = signNumber,
                    SignName = new ItemText(sign.SignName)
                };
            }

            //if (container != null)
            //{
                scheduleDay.Worships.AddRange(container);
            //}

            return scheduleDay;
        }

        private Sign GetPredefinedTemplate(Sign sign)
        {
            if (sign.Number.HasValue)
            {
                return sign;
            }

            if (sign.Template != null)
            {
                return GetPredefinedTemplate(sign.Template);
            }

            return default(Sign);
        }

        public GetScheduleWeekResponse GetScheduleWeek(GetScheduleWeekRequest request)
        {
            OutputWeek week = new OutputWeek() 
            {
                Name = nameComposer.GetWeekName(request.TypiconId, request.Date)
            };

            GetScheduleDayRequest dayRequest = new GetScheduleDayRequest()
            {
                Date = request.Date,
                TypiconId = request.TypiconId,
                Handler = request.Handler,
                Language = request.Language,
                ThrowExceptionIfInvalid = request.ThrowExceptionIfInvalid,
                ApplyParameters = request.ApplyParameters,
                CheckParameters = request.CheckParameters
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
