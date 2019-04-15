using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Jobs.Scheduled
{
    public interface IJobScheduler
    {
        DateTime NextDate { get; }
    }
}
