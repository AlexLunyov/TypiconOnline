using NUnit.Framework;
using System;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Tests.Common;
using TypiconOnline.AppServices.Implementations;

namespace TypiconOnline.Domain.Tests.Rules.Handlers
{
    [TestFixture]
    public class ServiceSequenceHandlerTest
    {
        //[Test]
        //public void ServiceSequenceHandler_Working()
        //{
        //    GetScheduleDayRequest request = new GetScheduleDayRequest()
        //    {
        //        Date = new DateTime(2017, 11, 13),//DateTime.Today,
        //        Handler = new ServiceSequenceHandler(),
        //        TypiconId = 1,
        //        CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
        //    };

        //    ScheduleService scheduleService = ScheduleServiceFactory.Create();

        //    GetScheduleDayResponse response = scheduleService.GetScheduleDay(request);

        //    Assert.Pass();
        //}
    }
}
