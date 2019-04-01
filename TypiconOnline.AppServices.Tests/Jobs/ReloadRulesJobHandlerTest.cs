using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Jobs
{
    [TestFixture]
    class ReloadRulesJobHandlerTest
    {
        [TestCase(false, TypiconVersionStatus.Draft)]
        [TestCase(true, TypiconVersionStatus.Published)]
        public void ReloadRulesJob_Draft(bool isTrue, TypiconVersionStatus status)
        {
            var context = TypiconDbContextFactory.Create();

            var handler = new ReloadRulesJobHandler(GetConfigRepo(), context);

            var job = new ReloadRulesJob(1, status);

            handler.Execute(job);

            Assert.AreEqual(isTrue, job.Status == JobStatus.Finished);
        }

        private IConfigurationRepository GetConfigRepo()
        {
            var mock = new Mock<IConfigurationRepository>();
            mock.Setup(c => c.GetConfigurationValue(It.IsAny<string>()))
                .Returns(@"E:\Programming\Documentation\TypiconOnline.Documentation\XML");

            return mock.Object;
        }
    }
}
