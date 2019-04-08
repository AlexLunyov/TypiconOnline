using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IJobRepository
    {
        Result Create(IJob job);
        Result Recreate(IJob job);
        Result Start(IJob job);
        Result Finish(IJob job);
        Result Finish(IJob job, string message);
        Result Fail(IJob job, string message);
        IEnumerable<IJob> GetAll();
        IEnumerable<IJob> Get(int count);
    }
}
