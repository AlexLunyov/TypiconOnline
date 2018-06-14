using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.AppServices.Implementations
{
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
                TypiconId = request.TypiconId,
                Language = request.Language,
                ApplyParameters = request.ApplyParameters,
                CheckParameters = request.CheckParameters
                    .SetModeParam((mode == HandlingMode.AstronomicDay) ? HandlingMode.ThisDay : mode)
            };

            ScheduleDay scheduleDay = GetOrFillScheduleDay(settingsRequest, request.Handler);

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

        

        private ScheduleDay GetOrFillScheduleDay(ScheduleDataCalculatorRequest request, ScheduleHandler handler, ScheduleDay scheduleDay = null)
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
                Sign sign = response.Rule.Template.GetPredefinedTemplate();

                //Если settings.SignNumber определен в ModifiedRule, то назначаем его
                int signNumber = settings.SignNumber ?? (int)sign.Number;

                scheduleDay = new ScheduleDay
                {
                    //задаем имя дню
                    Name = nameComposer.Compose(request.Date, response.Rule.Template.Priority, settings.DayWorships, settings.Language),
                    Date = request.Date,
                    SignNumber = signNumber,
                    SignName = sign.SignName.FirstOrDefault(settings.Language.Name),
                };
            }

            //if (container != null)
            //{
                scheduleDay.Worships.AddRange(container);
            //}

            return scheduleDay;
        }

        public GetScheduleWeekResponse GetScheduleWeek(GetScheduleWeekRequest request)
        {
            ScheduleWeek week = new ScheduleWeek() 
            {
                Name = nameComposer.GetWeekName(request.Date, request.Language)
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
