using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query
{
    public class DataQueryProcessor : IDataQueryProcessor, IDisposable
    {
        private readonly Container container;
        private readonly Scope scope;

        public DataQueryProcessor(Container container)
        {
            this.container = container;

            scope = AsyncScopedLifestyle.BeginScope(container);
        }

        public void Dispose()
        {
            scope.Dispose();
        }

        [DebuggerStepThrough]
        public TResult Process<TResult>(IDataQuery<TResult> query)
        {
            var handlerType =
                typeof(IDataQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = container.GetInstance(handlerType);

            return handler.Handle((dynamic)query);
        }
    }
}
