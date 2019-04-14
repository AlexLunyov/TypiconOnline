using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.AppServices.Common;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.AppServices.Implementations
{
    public class OutputFormFactory : IOutputFormFactory
    {
        private readonly IScheduleDayNameComposer _nameComposer;
        private readonly ITypiconSerializer _typiconSerializer;
        private readonly IScheduleDataCalculator _dataCalculator;

        //заменить в дальнейшем на ServiceSequenceHandler
        //private readonly ScheduleHandler _handler = new ScheduleHandler();
        private readonly ScheduleHandler _handler;// = new ServiceSequenceHandler();

        public OutputFormFactory(IScheduleDataCalculator dataCalculator
            , IScheduleDayNameComposer nameComposer
            , ITypiconSerializer typiconSerializer
            , ScheduleHandler handler)
        {
            _dataCalculator = dataCalculator ?? throw new ArgumentNullException(nameof(dataCalculator));
            _nameComposer = nameComposer ?? throw new ArgumentNullException(nameof(nameComposer));
            _typiconSerializer = typiconSerializer ?? throw new ArgumentNullException(nameof(typiconSerializer));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typiconId">Id Устава</param>
        /// <param name="typiconVersionId">Версия Устава</param>
        /// <param name="date"></param>
        /// <returns></returns>
        public CreateOutputFormResponse Create(CreateOutputFormRequest req)
        {
            OutputDayInfo dayInfo = null;

            return InnerCreate(req, ref dayInfo, _dataCalculator);
        }

        public CreateOutputFormResponse Create(IScheduleDataCalculator dataCalculator, CreateOutputFormRequest req)
        {
            if (dataCalculator == null)
            {
                throw new ArgumentNullException(nameof(dataCalculator));
            }

            OutputDayInfo dayInfo = null;

            return InnerCreate(req, ref dayInfo, dataCalculator);
        }

        /// <summary>
        /// Возвращает неделю
        /// </summary>
        /// <param name="typiconVersionId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable<OutputForm> CreateWeek(CreateOutputFormWeekRequest req)
        {
            List<OutputForm> result = new List<OutputForm>();

            var dayReq = new CreateOutputFormRequest()
            {
                TypiconId = req.TypiconId,
                TypiconVersionId = req.TypiconVersionId,
                HandlingMode = HandlingMode.AstronomicDay
            };

            OutputDayInfo dayInfo = null;

            EachDayPerWeek.Perform(req.Date, date =>
            {
                dayReq.Date = date;

                var output = InnerCreate(dayReq, ref dayInfo, _dataCalculator);

                result.Add(output.Form);
            });

            return result;
        }

        private CreateOutputFormResponse InnerCreate(CreateOutputFormRequest req, ref OutputDayInfo dayInfo, IScheduleDataCalculator dataCalculator)
        {
            if (dayInfo == null)
            {
                //Формируем данные для обработки
                dayInfo = GetOutputDayInfo(dataCalculator, new ScheduleDataCalculatorRequest()
                {
                    TypiconVersionId = req.TypiconVersionId,
                    Date = req.Date
                });
            }

            if (req.HandlingMode == HandlingMode.DayBefore 
                || req.HandlingMode == HandlingMode.All)
            {
                //добавляем DayBefore
                dayInfo.Day.Worships.AddRange(dayInfo.ScheduleResults.DayBefore);
            }

            if (req.HandlingMode == HandlingMode.ThisDay 
                || req.HandlingMode == HandlingMode.All
                || req.HandlingMode == HandlingMode.AstronomicDay)
            {
                //добавляем ThisDay
                dayInfo.Day.Worships.AddRange(dayInfo.ScheduleResults.ThisDay);
            }

            var localDayInfo = dayInfo;

            //добавляем AstronomicDay
            if (req.HandlingMode == HandlingMode.AstronomicDay)
            {
                //Формируем данные для обработки от следующего дня
                dayInfo = GetOutputDayInfo(dataCalculator, new ScheduleDataCalculatorRequest()
                {
                    TypiconVersionId = req.TypiconVersionId,
                    Date = req.Date.AddDays(1)
                });

                //складываем значения
                localDayInfo.Merge(dayInfo);
            }

            string definition = _typiconSerializer.Serialize(localDayInfo.Day);

            var outputForm = new OutputForm(req.TypiconId, req.Date, definition);

            //Добавить ссылки на службы
            outputForm.OutputFormDayWorships = GetOutputFormDayWorships(outputForm, localDayInfo.DayWorships);

            return new CreateOutputFormResponse(outputForm, localDayInfo.Day, localDayInfo.BrokenConstraints);
        }

        /// <summary>
        /// Вычисляет свойства для заполнения выходной формы
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private OutputDayInfo GetOutputDayInfo(IScheduleDataCalculator dataCalculator, ScheduleDataCalculatorRequest request)
        {
            //Формируем данные для обработки
            var response = dataCalculator.Calculate(request);

            var settings = response.Settings;

            var brokenConstraints = GetBrokenConstraints(settings);

            _handler.Settings = settings;

            _handler.ClearResult();

            settings.RuleContainer.Interpret(_handler);

            var results = _handler.GetResults();

            var sign = response.Rule.Template.GetPredefinedTemplate();

            //Если settings.SignNumber определен в ModifiedRule, то назначаем его
            int signNumber = settings.SignNumber ?? sign.Number.Value;

            var scheduleDay = new OutputDay
            {
                //задаем имя дню
                Name = _nameComposer.Compose(request.Date, response.Rule.Template.Priority, settings.AllWorships),
                Date = request.Date,
                SignNumber = signNumber,
                SignName = new ItemText(sign.SignName),
            };

            return new OutputDayInfo(scheduleDay, settings.AllWorships, results, brokenConstraints);
        }

        private IEnumerable<BusinessConstraint> GetBrokenConstraints(RuleHandlerSettings settings)
        {
            var constraints = new List<BusinessConstraint>();

            if (settings.RuleContainer != null)
            {
                constraints.AddRange(settings.RuleContainer.GetBrokenConstraints());
            }

            if (settings.Addition != null)
            {
                constraints.AddRange(GetBrokenConstraints(settings.Addition));
            }

            return constraints;
        }

        /// <summary>
        /// Проверяет, валидны ли десериализованные правила
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private bool AreSettingsRuleContainersValid(RuleHandlerSettings settings)
        {
            bool result = settings.RuleContainer?.IsValid == true;

            if (result && settings.Addition != null)
            {
                result &= AreSettingsRuleContainersValid(settings.Addition);
            }

            return result;
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
                    //DayWorship = worship,
                    DayWorshipId = worship.Id
                };

                result.Add(ofdw);
            }

            return result;
        }

        
    }
}
