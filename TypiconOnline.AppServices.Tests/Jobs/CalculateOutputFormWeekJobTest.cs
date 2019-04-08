using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Jobs
{
    [TestFixture]
    public class CalculateOutputFormWeekJobTest
    {
        [Test]
        public void CalculateOutputFormYearJob_Failed()
        {
            var date = new DateTime(2019, 2, 1);

            var dbContext = TypiconDbContextFactory.Create();
            var jobRepo = new JobRepository();
            var handler = Build(dbContext, jobRepo);

            var job = new CalculateOutputFormWeekJob(1, 1, date);
            jobRepo.Create(job);

            var task = handler.ExecuteAsync(job);
            task.Wait();
            
            var outputForms = OutputFormsFactory.Create(dbContext);

            var week = outputForms.GetWeek(1, date, "cs-ru");

            Assert.AreEqual(false, week.Success);
        }

        [Test]
        public void CalculateOutputFormYearJob_Success()
        {
            var date = new DateTime(2019, 2, 1);

            var dbContext = TypiconDbContextFactory.Create();
            var jobRepo = new JobRepository();

            var yearHandler = CalculateModifiedYearJobHandlerTest.Build(dbContext, jobRepo);
            var yearJob = new CalculateModifiedYearJob(1, 2019);
            jobRepo.Create(yearJob);

            Task task = yearHandler.ExecuteAsync(yearJob);
            task.Wait();

            var weekHandler = Build(dbContext, jobRepo);
            var weekJob = new CalculateOutputFormWeekJob(1, 1, date);
            jobRepo.Create(weekJob);

            task = weekHandler.ExecuteAsync(weekJob);
            task.Wait();

            var outputForms = OutputFormsFactory.Create(dbContext);

            var week = outputForms.GetWeek(1, date, "cs-ru");

            Assert.AreEqual(true, week.Success);
        }

        public static CalculateOutputFormWeekJobHandler Build(TypiconDBContext dbContext, JobRepository jobRepo) 
        {
            var query = DataQueryProcessorFactory.Create(dbContext);

            var serializerRoot = TestRuleSerializer.Create(dbContext);

            var settingsFactory = new RuleHandlerSettingsFactory(serializerRoot);

            var outputFormFactory = new OutputFormFactory(new ScheduleDataCalculator(query, settingsFactory)
                , new ScheduleDayNameComposer(query)
                , serializerRoot.TypiconSerializer
                , new ServiceSequenceHandler());

            return new CalculateOutputFormWeekJobHandler(dbContext, outputFormFactory, jobRepo);
        }
    }
}
