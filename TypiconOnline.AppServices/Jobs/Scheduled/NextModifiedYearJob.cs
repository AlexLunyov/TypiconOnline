using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs.Scheduled
{
    public class NextModifiedYearJob : ScheduledJob
    {
        public NextModifiedYearJob(IJobScheduler jobScheduler, int yearsCount) : base(jobScheduler)
        {
            if (yearsCount < 1)
            {
                throw new IndexOutOfRangeException(nameof(yearsCount));
            }

            YearsCount = yearsCount;
        }

        /// <summary>
        /// Количество недель, которые нужно просчитать вперед
        /// </summary>
        public int YearsCount { get; }

        public override bool Equals(IJob other)
        {
            return (other is NextModifiedYearJob);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + YearsCount.GetHashCode();
                return hash;
            }
        }
    }
}
