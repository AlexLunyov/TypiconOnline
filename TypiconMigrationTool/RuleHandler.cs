using System;
using System.Collections.Generic;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Schedule;
using TypiconOnline.Domain.Services;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.AppServices.Services;
using TypiconMigrationTool.Tests;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Interfaces;

namespace TypiconMigrationTool
{
    public class RuleHandler
    {
        IUnitOfWork _unitOfWork;

        TypiconEntity _typiconEntity;

        IScheduleService _scheduleService;

        public RuleHandler(IUnitOfWork unitOfWork)
        {
            Initialize(unitOfWork);
        }

        private void Initialize(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");

            _typiconEntity = _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            if (_typiconEntity == null)
                throw new NullReferenceException("TypiconEntity");

            var easterContext = new EasterFakeContext(new DateTime(2010, 4, 7));

            _scheduleService = ScheduleServiceFactory.Create(_unitOfWork, easterContext);
        }

        public ScheduleDay GetDay(DateTime date)
        {
            GetScheduleDayRequest request = new GetScheduleDayRequest()
            {
                Date = date,
                Typicon = _typiconEntity,
                Handler = new ScheduleHandler(),
                CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.All)
            };

            GetScheduleDayResponse response = _scheduleService.GetScheduleDay(request);

            return response?.Day;
        }
    }
}
