using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Jobs
{
    [TestFixture]
    public class CalculateOutputFormYearJobTest
    {
        [Test]
        public void CalculateOutputFormYearJob_Test()
        {
            var date = DateTime.Now;

            var handler = Build();

            var task = handler.ExecuteAsync(new CalculateOutputFormYearJob(1, 1, 2019));

            Assert.AreEqual(TaskStatus.RanToCompletion, task.Status);
        }

        public static CalculateOutputFormYearJobHandler Build()
        {
            var dbContext = TypiconDbContextFactory.Create();

            var query = DataQueryProcessorFactory.Create(dbContext);
            var command = CommandProcessorFactory.Create(dbContext);

            var serializerRoot = TestRuleSerializer.Create(dbContext);

            var settingsFactory = new RuleHandlerSettingsFactory(serializerRoot);

            var outputFormFactory = new OutputFormFactory(new ScheduleDataCalculator(query, settingsFactory)
                , new ScheduleDayNameComposer(query)
                , serializerRoot.TypiconSerializer);

            return new CalculateOutputFormYearJobHandler(outputFormFactory, command);
        }
    }
}
