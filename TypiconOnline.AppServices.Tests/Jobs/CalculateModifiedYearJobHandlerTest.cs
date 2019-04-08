using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Jobs
{
    [TestFixture]
    public class CalculateModifiedYearJobHandlerTest
    {
        [Test]
        public void CalculateModifiedYearJob_Test()
        {
            var dbContext = TypiconDbContextFactory.Create();
            var jobRepo = new JobRepository();
            var jobHandler = Build(dbContext, jobRepo);

            var job = new CalculateModifiedYearJob(1, 2019);

            jobRepo.Create(job);

            var task = jobHandler.ExecuteAsync(job);
            task.Wait();

            Assert.AreEqual(0, jobRepo.GetAll().Count());
        }

        public static CalculateModifiedYearJobHandler Build(TypiconDBContext dbContext, JobRepository jobRepo)
        {
            var query = DataQueryProcessorFactory.Create(dbContext);
            var command = CommandProcessorFactory.Create(dbContext);

            var serializerRoot = TestRuleSerializer.Create(dbContext);

            var settingsFactory = new RuleHandlerSettingsFactory(serializerRoot);

            return new CalculateModifiedYearJobHandler(dbContext, settingsFactory, jobRepo);
        }

    }
}
