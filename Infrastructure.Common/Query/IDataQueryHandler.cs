using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Query
{
    public interface IDataQueryHandler<TQuery, TResult> where TQuery : IDataQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
