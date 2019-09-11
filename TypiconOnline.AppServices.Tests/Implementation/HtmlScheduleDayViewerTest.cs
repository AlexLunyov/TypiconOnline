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
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Implementation
{
    [TestFixture]
    public class HtmlScheduleDayViewerTest
    {
        [Test]
        public void HtmlScheduleDayViewer_Test()
        {
            var queryProcessor = QueryProcessorFactory.Create();

            var scheduleDay = queryProcessor.Process(new OutputDayQuery(1, new DateTime(2017, 11, 13), new OutputFilter() { Language = "cs-ru" }));

            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData/scheduledayviewer.xslt");

            var confCacheDuration = Mock.Of<IConfigurationRepository>(c => c.GetConfigurationValue("ScheduleDayViewer_XsltFile") == path);

            var viewer = new HtmlScheduleDayViewer(new TypiconSerializer(), confCacheDuration);

            Assert.IsNotNull(viewer.Execute(scheduleDay.Value));
        }

        //[Test]
        //public void HtmlScheduleDayViewer_Serialize()
        //{
        //    var queryProcessor = QueryProcessorFactory.Create();

        //    var scheduleDay = queryProcessor.Process(new OutputDayQuery(1, new DateTime(2019, 05, 09), new OutputFilter() { Language = "cs-ru" }));

        //    string path = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData/scheduledayviewer.xslt");

        //    var config = Mock.Of<IConfigurationRepository>(c => c.GetConfigurationValue("ScheduleDayViewer_XsltFile") == path);

        //    var viewer = new HtmlScheduleDayViewer(new TypiconSerializer(), config);

        //    Assert.IsNotNull(viewer.Execute(scheduleDay.Value[1]));
        //}
    }
}
