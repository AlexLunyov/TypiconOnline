using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Jobs
{
    public abstract class JobHandlerBase<TJob> where TJob : IJob
    {
        public JobHandlerBase(IJobRepository jobs)
        {
            Jobs = jobs ?? throw new ArgumentNullException(nameof(jobs));
        }

        protected IJobRepository Jobs { get; }

        public Task<Result> ExecuteAsync(TJob job)
        {
            Start(job);

            return DoTheJob(job);
        }

        protected abstract Task<Result> DoTheJob(TJob job);

        protected void Start(TJob job)
        {
            Jobs.Start(job);
        }

        protected Result Finish(TJob job)
        {
            Jobs.Finish(job);

            return Result.Ok();
        }

        protected Result Fail(TJob job, string err)
        {
            Jobs.Fail(job, err);

            return Result.Fail(err);
        }

        protected Result Recreate(TJob job, int delay = 5000)
        {
            Jobs.Recreate(job);

            return Result.Fail($"Задача отложена на {delay} миллисекунд");
        }
    }
}
