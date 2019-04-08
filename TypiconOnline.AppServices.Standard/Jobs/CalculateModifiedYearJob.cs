using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs
{
    public class CalculateModifiedYearJob : IJob
    {
        public CalculateModifiedYearJob(int typiconVersionId, int year) : base()
        {
            TypiconVersionId = typiconVersionId;
            Year = year;
        }

        public int TypiconVersionId { get; }
        public int Year { get; }

        public bool Equals(IJob other)
        {
            if (other is CalculateModifiedYearJob j)
            {
                return TypiconVersionId == j.TypiconVersionId && Year == j.Year;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + TypiconVersionId.GetHashCode();
                hash = hash * 23 + Year.GetHashCode();
                return hash;
            }
        }
    }
}
