using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs
{
    public class CalculateOutputFormWeekJob : IJob
    {
        public CalculateOutputFormWeekJob(int typiconId, int typiconVersionId, DateTime date) 
        {
            TypiconId = typiconId;
            TypiconVersionId = typiconVersionId;
            Date = date;
        }

        /// <summary>
        /// Id Устава
        /// </summary>
        public int TypiconId { get; }
        /// <summary>
        /// Версия Устава
        /// </summary>
        public int TypiconVersionId { get; }
        private DateTime _date;

        /// <summary>
        /// Всегда устанавливается понедельник 00:00 от указанной даты
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            private set
            {
                _date = value.Date;

                while (_date.DayOfWeek != DayOfWeek.Monday)
                {
                    _date = _date.AddDays(-1);
                }
            }
        }

        public bool Equals(IJob other)
        {
            if (other is CalculateOutputFormWeekJob j)
            {
                return TypiconId == j.TypiconId && TypiconVersionId == j.TypiconVersionId && Date == j.Date;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + TypiconId.GetHashCode();
                hash = hash * 23 + TypiconVersionId.GetHashCode();
                hash = hash * 23 + Date.GetHashCode();
                return hash;
            }
        }
    }
}
