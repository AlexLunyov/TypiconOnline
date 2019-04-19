using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs
{
    public class ApproveTypiconEntityJob : IJob
    {
        public ApproveTypiconEntityJob(int typiconId)
        {
            TypiconId = typiconId;
        }

        public int TypiconId { get; }

        public bool Equals(IJob other)
        {
            if (other is ApproveTypiconEntityJob j)
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
