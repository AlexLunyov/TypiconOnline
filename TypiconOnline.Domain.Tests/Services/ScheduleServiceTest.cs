using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Services;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Repository.EF;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Services
{
    [TestFixture]
    public class ScheduleServiceTest
    {
        [Test]
        public void ScheduleService_GetService()
        {
            var unitOfWork = UnitOfWorkFactory.Create();

            var typiconEntity = unitOfWork.Repository<TypiconEntity>().Get(c => c.Id == 1);

            ScheduleHandler handler = new ScheduleHandler();

            GetScheduleDayRequest request = new GetScheduleDayRequest()
            {
                Date = new DateTime(2018, 5, 21),//DateTime.Today,
                Handler = new ScheduleHandler(),
                Typicon = typiconEntity,
                CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
            };

            ScheduleService scheduleService = ScheduleServiceFactory.Create();

            GetScheduleDayResponse response = scheduleService.GetScheduleDay(request);

            //unitOfWork.SaveChanges();

            Assert.IsNotNull(response.Day.Schedule);

            StringBuilder builder = new StringBuilder();

            builder.AppendLine(response.Day.Date.ToShortDateString() + " " + response.Day.Name);

            foreach (WorshipRuleViewModel service in response.Day.Schedule)
            {
                builder.AppendLine(service.Time + " " + service.Name);
            }

            Assert.Pass(builder.ToString());
        }
    }
}
