using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Implementation
{
    [TestFixture]
    public class ScheduleOutputFormServiceTest
    {
        [Test]
        public void ScheduleOutputFormService_Test()
        {
            var service = Build();

            var day = service.GetScheduleDay(new GetScheduleDayRequest()
            {
                TypiconId = 1,
                Date = new DateTime(2019, 1, 1)
            });

            Assert.NotNull(day.Day);
        }

        private ScheduleOutputFormService Build()
        {
            var dbContext = TypiconDbContextFactory.Create();

            var query = DataQueryProcessorFactory.Create(dbContext);

            return new ScheduleOutputFormService(dbContext, new ScheduleDayNameComposer(query), new TypiconSerializer());
        }
    }
}
