using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Interfaces;

namespace TypiconOnline.AppServices.Jobs
{
    public class JobQueue : IQueue
    {
        private List<IJob> jobs = new List<IJob>();

        /// <summary>
        /// Выдает из очереди количество указанных элементов, начиная с первого
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<T> Extract<T>(int count) where T : IJob
        {
            if (count < 1) throw new ArgumentOutOfRangeException(nameof(count));

            var result = new List<T>();

            while (count > 0 && jobs.Count > 0)
            {
                lock (jobs)
                {
                    result.Add((T) jobs[0]);

                    jobs.RemoveAt(0);
                }
            }

            return result;
        }

        public IEnumerable<T> ExtractAll<T>() where T : IJob
        {
            var result = new List<T>(jobs.Cast<T>());

            lock (jobs)
            {
                jobs.Clear();
            }

            return result;
        }

        public void Send<T>(T job) where T : IJob
        {
            lock (jobs)
            {
                jobs.Add(job);
            }
        }
    }
}
