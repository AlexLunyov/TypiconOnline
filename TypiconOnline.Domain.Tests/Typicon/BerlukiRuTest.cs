using NUnit.Framework;
using System;
using System.Linq;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Typicon
{
    [TestFixture]
    public class BerlukiRuTest
    {
        [Test]
        public void BerlukiRu_Test()
        {
            DateTime date = new DateTime(2017, 09, 03);//DateTime.Now;

            if ((date.DayOfWeek == DayOfWeek.Sunday) && (date.Hour > 17))
            {
                date = date.AddDays(1);
            }

            GetScheduleWeekRequest weekRequest = new GetScheduleWeekRequest()
            {
                Date = date,
                TypiconId = 1,
                Handler = new ScheduleHandler(),
                CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
            };

            ScheduleService scheduleService = ScheduleServiceFactory.Create();

            GetScheduleWeekResponse weekResponse = scheduleService.GetScheduleWeek(weekRequest);

            HtmlScheduleWeekViewer htmlViewer = new HtmlScheduleWeekViewer();
            htmlViewer.Execute(weekResponse.Week);

            string resultString = htmlViewer.Execute(weekResponse.Week);

            weekRequest.Date = date.AddDays(7);

            weekResponse = scheduleService.GetScheduleWeek(weekRequest);
            resultString += htmlViewer.Execute(weekResponse.Week);

            Assert.Pass(resultString);
        }

        [Test]
        public void BerlukiRu_ComparingRequests()
        {
            DateTime date = new DateTime(2017, 9, 24);

            //сначала как в запросе в контроллере BerlukiRuController
            GetScheduleDayRequest dayRequest1 = new GetScheduleDayRequest()
            {
                Date = date,
                Handler = new ScheduleHandler(),
                TypiconId = 1,
                CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
            };

            ScheduleService scheduleService = ScheduleServiceFactory.Create();

            GetScheduleDayResponse dayResponse1 = scheduleService.GetScheduleDay(dayRequest1);

            //теперь как TypiconController
            GetScheduleDayRequest dayRequest2 = new GetScheduleDayRequest()
            {
                Date = date,
                TypiconId = 1,
                Handler = new ScheduleHandler(),
                CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
            };

            GetScheduleDayResponse dayResponse2 = scheduleService.GetScheduleDay(dayRequest2);

            GetScheduleWeekRequest weekRequest = new GetScheduleWeekRequest()
            {
                Date = date,
                TypiconId = 1,
                Handler = new ScheduleHandler(),
                CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
            };

            GetScheduleWeekResponse weekResponse = scheduleService.GetScheduleWeek(weekRequest);

            Assert.AreEqual(dayResponse1.Day.Name.Text, dayResponse2.Day.Name.Text);
            Assert.AreEqual(dayResponse1.Day.Name.Text, weekResponse.Week.Days.Last().Name.Text);
        }
    }
}
