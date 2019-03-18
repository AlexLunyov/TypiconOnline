using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.AppServices.Implementations
{
    public class OutputFormFactory : IOutputFormFactory
    {
        private readonly IScheduleDataCalculator _dataCalculator;
        private readonly IScheduleDayNameComposer _nameComposer;
        private readonly ITypiconSerializer _typiconSerializer;

        //заменить в дальнейшем на ServiceSequenceHandler
        private readonly ScheduleHandler _handler = new ScheduleHandler();//ServiceSequenceHandler();

        public OutputFormFactory(IScheduleDataCalculator dataCalculator
            , IScheduleDayNameComposer nameComposer
            , ITypiconSerializer typiconSerializer)
        {
            _dataCalculator = dataCalculator ?? throw new ArgumentNullException(nameof(dataCalculator));
            _nameComposer = nameComposer ?? throw new ArgumentNullException(nameof(nameComposer));
            _typiconSerializer = typiconSerializer ?? throw new ArgumentNullException(nameof(typiconSerializer));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typiconId">Id Устава</param>
        /// <param name="typiconVersionId">Версия Устава</param>
        /// <param name="date"></param>
        /// <returns></returns>
        public (OutputForm, ScheduleDay) Create(OutputFormCreateRequest req)
        {
            var scheduleInfo = CreateScheduleInfo(req.TypiconVersionId, req.Date, req.HandlingMode);

            string definition = _typiconSerializer.Serialize(scheduleInfo.Day);

            var result = new OutputForm(req.TypiconId, req.Date, definition);

            //Добавить ссылки на службы
            result.OutputFormDayWorships = GetOutputFormDayWorships(result, scheduleInfo.Dayworships);

            return (result, scheduleInfo.Day);
        }

        

        private (ScheduleDay Day, IEnumerable<DayWorship> Dayworships) CreateScheduleInfo(int typiconVersionId, DateTime date, HandlingMode handlingMode)
        {
            //находим метод обработки дня
            HandlingMode mode = handlingMode;

            //Формируем данные для обработки
            var settingsRequest = new ScheduleDataCalculatorRequest()
            {
                TypiconVersionId = typiconVersionId,
                Date = date,
                CheckParameters = GetMode((mode == HandlingMode.AstronomicDay) ? HandlingMode.ThisDay : mode)
            };

            var scheduleInfo = GetOrFillScheduleInfo(settingsRequest);

            var dayWorships = new List<DayWorship>(scheduleInfo.Dayworships);

            if (mode == HandlingMode.AstronomicDay)
            {
                //ищем службы следующего дня с маркером IsDayBefore == true
                settingsRequest.Date = date.AddDays(1);
                settingsRequest.CheckParameters = settingsRequest.CheckParameters.SetModeParam(HandlingMode.DayBefore);

                scheduleInfo = GetOrFillScheduleInfo(settingsRequest, scheduleInfo.Day);
            }

            return (scheduleInfo.Day, dayWorships);
        }

        private (ScheduleDay Day, IEnumerable<DayWorship> Dayworships) GetOrFillScheduleInfo(ScheduleDataCalculatorRequest request, ScheduleDay scheduleDay = null)
        {
            //Формируем данные для обработки
            var response = _dataCalculator.Calculate(request);

            var settings = response.Settings;

            _handler.Settings = settings;

            settings.RuleContainer.Interpret(_handler);

            var container = _handler.GetResult();

            if (scheduleDay == null)
            {
                //Sign sign = (settings.Rule is Sign s) ? s : GetTemplateSign(settings.Rule.Template);
                var sign = GetPredefinedTemplate(response.Rule.Template);

                //Если settings.SignNumber определен в ModifiedRule, то назначаем его
                int signNumber = settings.SignNumber ?? sign.Number.Value;

                scheduleDay = new ScheduleDay
                {
                    //задаем имя дню
                    Name = _nameComposer.Compose(request.Date, response.Rule.Template.Priority, settings.DayWorships, settings.Language),
                    Date = request.Date,
                    SignNumber = signNumber,
                    SignName = sign.SignName.FirstOrDefault(settings.Language.Name),
                };
            }

            //if (container != null)
            //{
            scheduleDay.Worships.AddRange(container);
            //}

            return (scheduleDay, settings.DayWorships);
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

        /// <summary>
        /// Возвращает ссылки на используемые службы дял составления выходной формы
        /// </summary>
        /// <param name="outputForm"></param>
        /// <param name="dayworships"></param>
        /// <returns></returns>
        private List<OutputFormDayWorship> GetOutputFormDayWorships(OutputForm outputForm, IEnumerable<DayWorship> dayworships)
        {
            var result = new List<OutputFormDayWorship>();

            foreach (var worship in dayworships)
            {
                var ofdw = new OutputFormDayWorship()
                {
                    OutputForm = outputForm,
                    DayWorship = worship
                };

                result.Add(ofdw);
            }

            return result;
        }

        private CustomParamsCollection<IRuleCheckParameter> GetThisDay() =>
            new CustomParamsCollection<IRuleCheckParameter>() { new WorshipRuleCheckModeParameter() { Mode = HandlingMode.ThisDay } };

        private CustomParamsCollection<IRuleCheckParameter> GetDayBefore()
            => new CustomParamsCollection<IRuleCheckParameter>() { new WorshipRuleCheckModeParameter() { Mode = HandlingMode.DayBefore } };

        private CustomParamsCollection<IRuleCheckParameter> GetMode(HandlingMode mode)
            => new CustomParamsCollection<IRuleCheckParameter>() { new WorshipRuleCheckModeParameter() { Mode = mode } };
    }
}
