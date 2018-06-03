using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query
{
    public sealed class DataQueryProcessor : IDataQueryProcessor
    {
        private readonly Container container;

        public DataQueryProcessor(Container container)
        {
            this.container = container;
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
