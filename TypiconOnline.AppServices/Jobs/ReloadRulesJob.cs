using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs
{
    public class ReloadRulesJob : IJob
    {
        public ReloadRulesJob(int typiconId, TypiconVersionStatus status) 
        {
            TypiconId = typiconId;
            TypiconVersionStatus = status;
        }

        public int TypiconId { get; }
        public TypiconVersionStatus TypiconVersionStatus { get; }

        public bool Equals(IJob other)
        {
            if (other is ReloadRulesJob j)
            {
                return TypiconId == j.TypiconId && TypiconVersionStatus == j.TypiconVersionStatus;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + TypiconId.GetHashCode();
                hash = hash * 23 + TypiconVersionStatus.GetHashCode();
                return hash;
            }
        }
    }

    public enum TypiconVersionStatus { Draft = 0, Published = 1 }
}
