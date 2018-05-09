using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Implementation
{
    [TestFixture]
    public class HtmlScheduleDayViewerTest
    {
        [Test]
        public void TestMethod()
        {
            var unitOfWork = UnitOfWorkFactory.Create();

            var typiconEntity = unitOfWork.Repository<TypiconEntity>().Get(c => c.Id == 1);

            var service = ScheduleServiceFactory.Create(unitOfWork);

            var scheduleDay = service.GetScheduleDay(new GetScheduleDayRequest()
            {
                Handler = new ServiceSequenceHandler(),
                Date = new DateTime(2017, 11, 13),
                Typicon = typiconEntity
            });

            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData/scheduledayviewer.xslt");

            var confCacheDuration = Mock.Of<IConfigurationRepository>(c => c.GetConfigurationValue("ScheduleDayViewer_XsltFile") == path);

            var viewer = new HtmlScheduleDayViewer(new TypiconSerializer(), confCacheDuration);

            Assert.IsNotNull(viewer.Execute(scheduleDay.Day));
        }
    }
}
