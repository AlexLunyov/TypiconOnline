using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs
{
    public class ReloadRulesJob : JobBase
    {
        public ReloadRulesJob(int typiconId, TypiconVersionStatus status) : base()
        {
            TypiconId = typiconId;
            TypiconVersionStatus = status;
        }

        public int TypiconId { get; }
        public TypiconVersionStatus TypiconVersionStatus { get; }
    }

    public enum TypiconVersionStatus { Draft = 0, Published = 1 }
}
