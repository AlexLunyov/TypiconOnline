using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IImportManager<TDomain, TProjection>
    {
        Result<TDomain> Import(TProjection projection);
    }
}
