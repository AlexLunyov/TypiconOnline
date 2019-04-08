using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Jobs
{
    public class JobRepository : IJobRepository
    {
        private Dictionary<IJob, JobStateHolder> jobs = new Dictionary<IJob, JobStateHolder>();

        public Result Create(IJob job)
        {
            lock (jobs)
            {
                if (!jobs.ContainsKey(job))
                {
                    jobs.Add(job, new JobStateHolder());
                
                    return Result.Ok();
                }
                else
                {
                    return Result.Fail("Задача уже имеется в очереди.");
                }
            }
        }

        public Result Recreate(IJob job)
        {
            return Update(job, new JobStateHolder());
        }

        public Result Start(IJob job)
        {
            return Update(job, new JobStateHolder() { Status = JobStatus.Started, BDate = DateTime.Now });
        }

        public Result Finish(IJob job) => Finish(job, string.Empty);

        public Result Finish(IJob job, string message)
        {
            //return Update(job, new JobStateHolder() { Status = JobStatus.Finished, EDate = DateTime.Now, StatusMessage = message });
            return Remove(job);
        }

        public Result Fail(IJob job, string message) 
        {
            //return Update(job, new JobStateHolder() { Status = JobStatus.Failed, EDate = DateTime.Now, StatusMessage = message });
            return Remove(job);
        }

        private Result Update(IJob job, JobStateHolder jobState) 
        {
            lock (jobs)
            {
                if (jobs.ContainsKey(job))
                {
                
                        jobs[job] = jobState;
                

                    return Result.Ok();
                }
                else
                {
                    return Result.Fail("Указанная задача не найдена в очереди.");
                }
            }
        }

        private Result Remove<T>(T job) where T: IJob
        {
            lock (jobs)
            {
                return jobs.Remove(job) ? Result.Ok() : Result.Fail("Указанная задача не найдена в очереди.");
            }
        }
        public IEnumerable<IJob> GetAll()
        {
            return (from i in jobs
                    where i.Value.Status == JobStatus.Created
                    orderby i.Value.CDate
                    select i.Key);
        }

        public IEnumerable<IJob> Get(int count) 
        {
            return GetAll().Take(count);
        }
    }
}
