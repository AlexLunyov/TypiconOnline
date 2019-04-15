using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Jobs.Scheduled;

namespace TypiconOnline.AppServices.Jobs.Scheduled
{
    public abstract class ScheduledJob : IJob 
    {
        private readonly IJobScheduler _jobScheduler;
        public ScheduledJob(IJobScheduler jobScheduler)
        {
            _jobScheduler = jobScheduler ?? throw new ArgumentNullException(nameof(jobScheduler));
        }

        public DateTime NextDate => _jobScheduler.NextDate;

        public abstract bool Equals(IJob other);
    }
}
