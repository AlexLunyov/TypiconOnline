using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query
{
    public class QueryProcessor: IQueryProcessor, IDisposable
    {
        private readonly Container container;
        private readonly Scope scope;

        public QueryProcessor(Container container)
        {
            this.container = container;
            scope = AsyncScopedLifestyle.BeginScope(container);
        }

        [DebuggerStepThrough]
        public TResult Process<TResult>(IQuery<TResult> query)
        {
            var handlerType =
                typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = container.GetInstance(handlerType);

            return handler.Handle((dynamic)query);
        }

        public void Dispose()
        {
            scope.Dispose();
        }
    }
}
