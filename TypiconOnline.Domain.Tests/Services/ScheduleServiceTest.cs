using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;

using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Services
{
    [TestFixture]
    public class ScheduleServiceTest
    {
        [Test]
        public void ScheduleService_GetService()
        {
            GetScheduleDayRequest request = new GetScheduleDayRequest()
            {
                Date = new DateTime(2018, 5, 21),//DateTime.Today,
                Handler = new ScheduleHandler(),
                TypiconId = 1,
                CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
            };

            ScheduleService scheduleService = ScheduleServiceFactory.Create();

            GetScheduleDayResponse response = scheduleService.GetScheduleDay(request);

            //unitOfWork.SaveChanges();

            Assert.IsNotNull(response.Day);

            StringBuilder builder = new StringBuilder();

            builder.AppendLine(response.Day.Date.ToShortDateString() + " " + response.Day.Name);

            foreach (var service in response.Day.Worships)
            {
                builder.AppendLine(service.Time + " " + service.Name);
            }

            Assert.Pass(builder.ToString());
        }
    }
}
