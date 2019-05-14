using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Query
{
    public abstract class QueryDecoratorBase<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        protected readonly IQueryHandler<TQuery, TResult> Decorated;

        public QueryDecoratorBase(IQueryHandler<TQuery, TResult> decorated)
        {
            Decorated = decorated;
        }

        public abstract TResult Handle(TQuery query);
    }
}
