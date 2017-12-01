using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.AppServices.Services;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Services
{
    public class ScheduleService : IScheduleService
    {
        //ITypiconEntityService _typiconEntityService;
        IRuleHandlerSettingsFactory _settingsFactory = new RuleHandlerSettingsFactory();
        //IModifiedRuleService _modifiedRuleService;
        //IRuleHandler _ruleHandler;
        //BookStorage _bookStorage;
        IScheduleDayNameComposer nameComposer;
        IRuleSerializerRoot _ruleSerializer;

        public ScheduleService(/*ITypiconEntityService typiconEntityService
            , IRuleHandlerSettingsFactory settingsFactory
            ,*/ IRuleSerializerRoot ruleSerializer
            //, IModifiedRuleService modifiedRuleService
            //, IRuleHandler ruleHandler
            )
        {
            //_typiconEntityService = typiconEntityService ?? throw new ArgumentNullException("ITypiconEntityService");
            //_settingsFactory = settingsFactory ?? throw new ArgumentNullException("IRuleHandlerSettingsFactory");
            //_modifiedRuleService = modifiedRuleService ?? throw new ArgumentNullException("modifiedRuleService");
            _ruleSerializer = ruleSerializer ?? throw new ArgumentNullException("IRuleSerializerRoot");

            nameComposer = new ScheduleDayNameComposer(_ruleSerializer.BookStorage.Oktoikh);

            //_ruleHandler = ruleHandler ?? throw new ArgumentNullException("IRuleHandler");
        }

        public GetScheduleDayResponse GetScheduleDay(GetScheduleDayRequest request)
        {
            //находим метод обработки дня
            HandlingMode mode = request.CheckParameters.GetMode();
            
            //Формируем данные для обработки
            var settingsRequest = new GetRuleSettingsRequest()
            {
                Date = request.Date,
                Language = request.Language,
                ApplyParameters = request.ApplyParameters,
                CheckParameters = request.CheckParameters
                    .SetModeParam((mode == HandlingMode.AstronimicDay) ? HandlingMode.ThisDay : mode)
            };

            ScheduleDay scheduleDay = GetOrFillScheduleDay(settingsRequest, request.Typicon, request.Handler, request.ConvertSignToHtmlBinding);

            if (mode == HandlingMode.AstronimicDay)
            {
                //ищем службы следующего дня с маркером IsDayBefore == true
                settingsRequest.Date = request.Date.AddDays(1);
                settingsRequest.CheckParameters = settingsRequest.CheckParameters.SetModeParam(HandlingMode.DayBefore);

                scheduleDay = GetOrFillScheduleDay(settingsRequest, request.Typicon, request.Handler, request.ConvertSignToHtmlBinding, scheduleDay);
            }

            return new GetScheduleDayResponse()
            {
                Day = scheduleDay
            };
        }

        

        private ScheduleDay GetOrFillScheduleDay(GetRuleSettingsRequest request, TypiconEntity typicon,
            ScheduleHandler handler, bool convertSignNumber, ScheduleDay scheduleDay = null)
        {
            //заполняем Правила и день Октоиха
            request.MenologyRule = typicon.GetMenologyRule(request.Date);
            request.TriodionRule = typicon.GetTriodionRule(request.Date);
            request.ModifiedRule = typicon.GetModifiedRuleHighestPriority(request.Date, _ruleSerializer);
            request.OktoikhDay = _ruleSerializer.BookStorage.Oktoikh.Get(request.Date);

            //Формируем данные для обработки
            var settings = _settingsFactory.Create(request);

            handler.Settings = settings;

            settings.Rule.GetRule(_ruleSerializer).Interpret(request.Date, handler);

            ContainerViewModel container = handler.GetResult();

            if (scheduleDay == null)
            {
                //Sign sign = (settings.Rule is Sign s) ? s : GetTemplateSign(settings.Rule.Template);
                Sign sign = GetRootAdditionSign(settings);

                int signNumber = (convertSignNumber) ? SignMigrator.GetOldId(k => k.Value.NewID == sign.Number) : sign.Number;

                if (request.Date.DayOfWeek == DayOfWeek.Sunday && sign.Priority > 3)
                {
                    //TODO: жесткая привязка к номеру знака воскресного дня
                    signNumber = 6;// SignMigrator.GetOldId(k => k.Value.Name == "Воскресный день");
                }

                scheduleDay = new ScheduleDay
                {
                    //задаем имя дню
                    Name = nameComposer.Compose(settings, request.Date),
                    Date = request.Date,
                    SignNumber = signNumber,
                    SignName = sign.SignName[settings.Language],
                };
            }

            if (container != null)
            {
                scheduleDay.Schedule.ChildElements.AddRange(container.ChildElements);
            }

            return scheduleDay;
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

        /// <summary>
        /// Возвращает Знак службы, помеченный как Предустановленный
        /// Используется для отображения предустановленных знаков службы в расписании
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        private Sign GetTemplateSign(Sign sign)
        {
            return (sign.IsTemplate || sign.Template == null) ? sign : GetTemplateSign(sign.Template);
        }

        private Sign GetRootAdditionSign(RuleHandlerSettings settings)
        {
            if (settings.Addition == null)
            {
                return GetTemplateSign((settings.Rule is Sign s) ? s : settings.Rule.Template);
            }
            
            return GetRootAdditionSign(settings.Addition);
        }

        public GetScheduleWeekResponse GetScheduleWeek(GetScheduleWeekRequest request)
        {
            ScheduleWeek week = new ScheduleWeek() 
            {
                Name = OktoikhCalculator.GetWeekName(request.Date, false)
            };

            GetScheduleDayRequest dayRequest = new GetScheduleDayRequest()
            {
                Date = request.Date,
                Typicon = request.Typicon,
                Handler = request.Handler,
                Language = request.Language,
                ThrowExceptionIfInvalid = request.ThrowExceptionIfInvalid,
                ConvertSignToHtmlBinding = request.ConvertSignToHtmlBinding,
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
