using System;
using System.Collections.Generic;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Easter;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Schedule;
using TypiconOnline.Domain.Services;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconMigrationTool
{
    public class RuleHandler
    {
        IUnitOfWork _unitOfWork;

        TypiconEntity _typiconEntity;

        ScheduleService _scheduleService;

        public RuleHandler(IUnitOfWork unitOfWork)
        {
            Initialize(unitOfWork);
        }

        private void Initialize(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;

            _typiconEntity = _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            if (_typiconEntity == null)
                throw new NullReferenceException("TypiconEntity");

            InitializeEasterRepositiry();

            //EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            _scheduleService = new ScheduleService();
        }

        public void InitializeEasterRepositiry()
        {
            DateTime blagovDate = new DateTime(2010, 4, 7);

            List<EasterItem> easters = new List<EasterItem>()
            {
                new EasterItem() { Date = blagovDate.AddDays(-29) },
                new EasterItem() { Date = blagovDate.AddDays(-22).AddYears(1) },
                new EasterItem() { Date = blagovDate.AddDays(-28).AddYears(2) },
                new EasterItem() { Date = blagovDate.AddDays(-27).AddYears(3) },
                new EasterItem() { Date = blagovDate.AddDays(-20).AddYears(4) },
                new EasterItem() { Date = blagovDate.AddDays(-13).AddYears(5) },
                new EasterItem() { Date = blagovDate.AddDays(-25).AddYears(6) },
                new EasterItem() { Date = blagovDate.AddDays(-21).AddYears(7) },
                new EasterItem() { Date = blagovDate.AddDays(-14).AddYears(8) },
                new EasterItem() { Date = blagovDate.AddDays(-18).AddYears(9) },
                new EasterItem() { Date = blagovDate.AddDays(-17).AddYears(10) },
                new EasterItem() { Date = blagovDate.AddDays(-16).AddYears(11) },
                new EasterItem() { Date = blagovDate.AddDays(-15).AddYears(12) },
                new EasterItem() { Date = blagovDate.AddDays(-14).AddYears(13) },
                new EasterItem() { Date = blagovDate.AddDays(-13).AddYears(14) },
                new EasterItem() { Date = blagovDate.AddDays(-12).AddYears(15) },
                new EasterItem() { Date = blagovDate.AddDays(-11).AddYears(16) },
                new EasterItem() { Date = blagovDate.AddDays(-10).AddYears(17) },
                new EasterItem() { Date = blagovDate.AddDays(-9).AddYears(18) },
                new EasterItem() { Date = blagovDate.AddDays(-8).AddYears(19) },
                new EasterItem() { Date = blagovDate.AddDays(-7).AddYears(20) },
                new EasterItem() { Date = blagovDate.AddDays(-6).AddYears(21) },
                new EasterItem() { Date = blagovDate.AddDays(-5).AddYears(22) },
                new EasterItem() { Date = blagovDate.AddDays(-4).AddYears(23) },
                new EasterItem() { Date = blagovDate.AddDays(-3).AddYears(24) },
                new EasterItem() { Date = blagovDate.AddDays(-2).AddYears(25) },
                new EasterItem() { Date = blagovDate.AddDays(-1).AddYears(26) },
                new EasterItem() { Date = blagovDate.AddDays(0).AddYears(27) },
                new EasterItem() { Date = blagovDate.AddDays(1).AddYears(28) },
                new EasterItem() { Date = blagovDate.AddDays(1).AddYears(29) },
                new EasterItem() { Date = blagovDate.AddDays(2).AddYears(30) },
                new EasterItem() { Date = blagovDate.AddDays(3).AddYears(31) },
            };

            EasterStorage.Instance.EasterDays = easters;
        }


        public ScheduleDay GetDay(DateTime date)
        {
            GetScheduleDayRequest request = new GetScheduleDayRequest()
            {
                Date = date,
                Mode = HandlingMode.All,
                TypiconEntity = _typiconEntity,
                Handler = new ScheduleHandler()
            };

            GetScheduleDayResponse response = _scheduleService.GetScheduleDay(request);

            return (response != null) ? response.Day : null;
        }
    }
}
