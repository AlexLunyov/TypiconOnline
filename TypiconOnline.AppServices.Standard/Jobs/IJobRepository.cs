using System.Collections.Generic;

namespace TypiconOnline.AppServices.Jobs
{
    public interface IJobRepository
    {
        IEnumerable<IJob> GetAllJobsToWork();
    }
}