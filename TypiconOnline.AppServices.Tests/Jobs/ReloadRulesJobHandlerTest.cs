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
        public void ReloadRulesJob_Test(bool isTrue, TypiconVersionStatus status)
        {
            var context = TypiconDbContextFactory.Create();

            var jobRepo = new JobRepository();

            var handler = new ReloadRulesJobHandler(GetConfigRepo(), context, jobRepo);

            var job = new ReloadRulesJob(1, status);

            jobRepo.Create(job);

            var task = handler.ExecuteAsync(job);
            task.Wait();

            Assert.AreEqual(0, jobRepo.GetAll().Count());
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
