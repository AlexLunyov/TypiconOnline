using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IProjector<TDomain, TProjection>
    {
        Result<TProjection> Project(TDomain domain);
    }
}
