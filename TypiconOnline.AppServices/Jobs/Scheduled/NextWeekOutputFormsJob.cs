using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs.Scheduled
{
    public class NextWeekOutputFormsJob : ScheduledJob
    {
        public NextWeekOutputFormsJob(IJobScheduler jobScheduler, int weeksForward) : base(jobScheduler)
        {
            if (weeksForward < 1)
            {
                throw new IndexOutOfRangeException(nameof(weeksForward));
            }

            WeeksForward = weeksForward;
        }

        /// <summary>
        /// Определение недели, которую нужно посчитать (на сколько недель вперед от текущей даты)
        /// </summary>
        public int WeeksForward { get; }

        public override bool Equals(IJob other)
        {
            return (other is NextWeekOutputFormsJob);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + WeeksForward.GetHashCode();
                return hash;
            }
        }
    }
}
