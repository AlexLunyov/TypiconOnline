using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Jobs
{
    [TestFixture]
    public class CalculateModifiedYearJobHandlerTest
    {
        [Test]
        public void CalculateModifiedYearJob_Test()
        {
            var jobHandler = Build();

            var task = jobHandler.ExecuteAsync(new CalculateModifiedYearJob(1, 2019));

            Assert.AreEqual(TaskStatus.RanToCompletion, task.Status);
        }

        public static CalculateModifiedYearJobHandler Build()
        {
            var dbContext = TypiconDbContextFactory.Create();

            var query = DataQueryProcessorFactory.Create(dbContext);
            var command = CommandProcessorFactory.Create(dbContext);

            var serializerRoot = TestRuleSerializer.Create(dbContext);

            var settingsFactory = new RuleHandlerSettingsFactory(serializerRoot);

            return new CalculateModifiedYearJobHandler(dbContext, settingsFactory);
        }

    }
}
