using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Interfaces
{
    public interface IGridQuery<T> : IQuery<Result<IQueryable<T>>> where T: IGridModel
    {
        string GetKey();
    }
}
