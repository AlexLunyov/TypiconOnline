using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Tests.Common;
using TypiconOnline.WebServices.Hosting;

namespace TypiconOnline.AppServices.Tests.Jobs
{
    [TestFixture]
    public class JobRepositoryTest
    {
        [Test]
        public void JobRepository_MoreThan()
        {
            var queue = new JobRepository();

            queue.Create(new CalculateModifiedYearJob(1, 2019));
            queue.Create(new CalculateModifiedYearJob(1, 2020));
            queue.Create(new CalculateModifiedYearJob(1, 2021));
            queue.Create(new CalculateModifiedYearJob(1, 2021));

            var c = queue.Reserve(6);

            Assert.AreEqual(3, c.Count());
        }

        [Test]
        public void HostingService_MySql()
        {
            var date = new DateTime(2019, 2, 1);

            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            optionsBuilder.UseMySql("server=localhost;UserId=root;Password=z2LDCiiEQFDBlkl3eZyb;database=typicondb;",
            //optionsBuilder.UseMySql("server=31.31.196.160;UserId=u0351_mysqluser;Password=gl7fdQ45GZyqydXrr2BZ;database=u0351320_typicondb;",
            mySqlOptions =>
            {
                mySqlOptions.ServerVersion(new Version(8, 0, 15), ServerType.MySql);
            });
            optionsBuilder.EnableSensitiveDataLogging();

            var dbContext = new TypiconDBContext(optionsBuilder.Options);

            var jobRepo = new JobRepository();

            //var yearHandler = CalculateModifiedYearJobHandlerTest.Build(dbContext, jobRepo);
            var yearJob = new CalculateModifiedYearJob(1, 2019);
            jobRepo.Create(yearJob);

            //Task task = yearHandler.ExecuteAsync(yearJob);
            //task.Wait();

            //var weekHandler = CalculateOutputFormWeekJobTest.Build(dbContext, jobRepo);
            var weekJob = new CalculateOutputFormWeekJob(1, 1, date);
            jobRepo.Create(weekJob);

            //task = weekHandler.ExecuteAsync(weekJob);
            //task.Wait();

            var service = new JobAsyncHostedService(jobRepo, CommandProcessorFactory.CreateJobProcessor(dbContext, jobRepo));

            var token = new CancellationTokenSource();

            Task.Factory.StartNew(() => service.StartAsync(token.Token));

            while (jobRepo.Create(weekJob).Failure)
            {
                Thread.Sleep(50);
            }

            token.Cancel();

            var queryProcessor = QueryProcessorFactory.Create();

            var week = queryProcessor.Process(new OutputWeekQuery(1, date, new OutputFilter() { Language = "cs-ru" }));

            Assert.AreEqual(true, week.Success);
        }

        [Test]
        public async Task HostingService_OutputForm()
        {
            var date = new DateTime(2019, 2, 1);

            var dbContext = TypiconDbContextFactory.Create();
            var jobRepo = new JobRepository();

            var yearHandler = CalculateModifiedYearJobHandlerTest.Build(dbContext, jobRepo);
            var yearJob = new CalculateModifiedYearJob(1, 2019);
            jobRepo.Create(yearJob);

            Task task = yearHandler.ExecuteAsync(yearJob);
            task.Wait();

            var weekHandler = CalculateOutputFormWeekJobTest.Build(dbContext, jobRepo);
            var weekJob = new CalculateOutputFormWeekJob(1, 1, date);
            jobRepo.Create(weekJob);

            task = weekHandler.ExecuteAsync(weekJob);
            task.Wait();

            var service = new JobAsyncHostedService(jobRepo, CommandProcessorFactory.Create(dbContext));

            await service.StartAsync(CancellationToken.None);

            while (jobRepo.GetAll().Count() > 0)
            {
                Thread.Sleep(50);
            }

            var queryProcessor = QueryProcessorFactory.Create();

            var week = queryProcessor.Process(new OutputWeekQuery(1, date, new OutputFilter() { Language = "cs-ru" }));

            Assert.AreEqual(true, week.Success);
        }
    }
}
