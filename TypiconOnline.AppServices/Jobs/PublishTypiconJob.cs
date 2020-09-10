using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs
{
    public class PublishTypiconJob : IJob
    {
        public PublishTypiconJob(int typiconId, bool deleteModifiedOutputDays)
        {
            TypiconId = typiconId;
            DeleteModifiedOutputDays = deleteModifiedOutputDays;
        }

        public int TypiconId { get; }

        /// <summary>
        /// Признак, удалять ли измененные вручную выходные формы расписания
        /// </summary>
        public bool DeleteModifiedOutputDays { get; }

        public bool Equals(IJob other)
        {
            if (other is PublishTypiconJob j)
            {
                return TypiconId == j.TypiconId;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + TypiconId.GetHashCode();
                return hash;
            }
        }
    }
}
