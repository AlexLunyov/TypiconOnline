﻿using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query
{
    public class DataQueryProcessor : IQueryProcessor//, IDisposable
    {
        protected Container Container { get; }

        public DataQueryProcessor(Container container)
        {
            Container = container;
        }

        //public void Dispose()
        //{
        //}

        [DebuggerStepThrough]
        public virtual TResult Process<TResult>(IQuery<TResult> query)
        {
            var handlerType =
                typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = Container.GetInstance(handlerType);

            return handler.Handle((dynamic)query);
        }
    }
}
