using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Query
{
    /// <summary>
    /// Средний по иерархии запрос.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IQuery<TResult> : IDataQuery<TResult>
    {
    }
}
