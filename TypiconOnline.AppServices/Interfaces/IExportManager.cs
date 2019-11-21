using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IExportManager<TDomain, TProjection>
    {
        Result<TProjection> Export(TDomain domain);
    }
}
