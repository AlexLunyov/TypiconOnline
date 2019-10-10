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
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Domain.WebQuery.OutputFiltering;

namespace TypiconOnline.Domain.Tests.Typicon
{
    [TestFixture]
    public class BerlukiRuTest
    {
        private const int TYPICON_ID = 1;

        [Test]
        public void BerlukiRu_Test()
        {
            DateTime date = new DateTime(2019, 09, 03);//DateTime.Now;

            if ((date.DayOfWeek == DayOfWeek.Sunday) && (date.Hour > 17))
            {
                date = date.AddDays(1);
            }

            var queryProcessor = DataQueryProcessorFactory.Create();

            var week = queryProcessor.Process(new OutputWeekQuery(TYPICON_ID, date, new OutputFilter() { Language = "cs-ru" }));

            HtmlScheduleWeekViewer htmlViewer = new HtmlScheduleWeekViewer();

            string resultString = htmlViewer.Execute(TYPICON_ID, week.Value);

            date = date.AddDays(7);

            week = queryProcessor.Process(new OutputWeekQuery(TYPICON_ID, date, new OutputFilter() { Language = "cs-ru" }));
            resultString += htmlViewer.Execute(TYPICON_ID, week.Value);

            Assert.Pass(resultString);
        }

        //[Test]
        //public void BerlukiRu_ComparingRequests()
        //{
        //    DateTime date = new DateTime(2017, 9, 24);

        //    //сначала как в запросе в контроллере BerlukiRuController
        //    GetScheduleDayRequest dayRequest1 = new GetScheduleDayRequest()
        //    {
        //        Date = date,
        //        Handler = new ScheduleHandler(),
        //        TypiconId = TYPICON_ID,
        //        CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
        //    };

        //    ScheduleService scheduleService = ScheduleServiceFactory.Create();

        //    GetScheduleDayResponse dayResponse1 = scheduleService.GetScheduleDay(dayRequest1);

        //    //теперь как TypiconController
        //    GetScheduleDayRequest dayRequest2 = new GetScheduleDayRequest()
        //    {
        //        Date = date,
        //        TypiconId = TYPICON_ID,
        //        Handler = new ScheduleHandler(),
        //        CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
        //    };

        //    GetScheduleDayResponse dayResponse2 = scheduleService.GetScheduleDay(dayRequest2);

        //    GetScheduleWeekRequest weekRequest = new GetScheduleWeekRequest()
        //    {
        //        Date = date,
        //        TypiconId = TYPICON_ID,
        //        Handler = new ScheduleHandler(),
        //        CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
        //    };

        //    GetScheduleWeekResponse weekResponse = scheduleService.GetScheduleWeek(weekRequest);

        //    Assert.AreEqual(dayResponse1.Day.Name.GetLocal(), dayResponse2.Day.Name.GetLocal());
        //    Assert.AreEqual(dayResponse1.Day.Name.GetLocal(), weekResponse.Week.Days.Last().Name.GetLocal());
        //}
    }
}
