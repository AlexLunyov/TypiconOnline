using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Query
{
    public interface IDataQueryProcessor
    {
        TResult Process<TResult>(IDataQuery<TResult> query);
    }
}
