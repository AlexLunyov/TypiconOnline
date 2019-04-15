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
        public JobRepository() { }

        public JobRepository(params IJob[] jobs)
        {
            if (jobs == null)
            {
                return;
            }

            foreach (var job in jobs)
            {
                Create(job);
            }
        }

        private Dictionary<IJob, JobStateHolder> Jobs { get; } = new Dictionary<IJob, JobStateHolder>();

        public Result Create(IJob job)
        {
            return Create(job, DateTime.Now);
        }

        public Result Create(IJob job, DateTime date)
        {
            lock (Jobs)
            {
                if (!Jobs.ContainsKey(job))
                {
                    Jobs.Add(job, new JobStateHolder(date));

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

        public Result Recreate(IJob job, int millisecondsDelay)
        {
            return Update(job, new JobStateHolder(DateTime.Now.AddMilliseconds(millisecondsDelay)));
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
            lock (Jobs)
            {
                if (Jobs.ContainsKey(job))
                {
                
                        Jobs[job] = jobState;
                

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
            lock (Jobs)
            {
                return Jobs.Remove(job) ? Result.Ok() : Result.Fail("Указанная задача не найдена в очереди.");
            }
        }
        public virtual IEnumerable<IJob> GetAll()
        {
            return InnerGetAll().Select(c => c.Key);
        }

        /// <summary>
        /// Возвращает задания, назначая им статус Reserved
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<IJob> Reserve(int count) 
        {
            //во избежание дублированныз запусков в асинхронном режиме
            //"стартуем" все задание при обращении к этому методу
            IEnumerable<KeyValuePair<IJob, JobStateHolder>> found = InnerGetAll().Take(count);

            foreach (var i in found)
            {
                i.Value.Status = JobStatus.Reserved;
            }

            return found.Select(c => c.Key);
        }

        private IEnumerable<KeyValuePair<IJob, JobStateHolder>> InnerGetAll()
        {
            return (from i in Jobs
                   where (i.Value.Status == JobStatus.Created
                       && DateTime.Now >= i.Value.CDate)
                   orderby i.Value.CDate
                   select i).ToList();
        }
    }
}
