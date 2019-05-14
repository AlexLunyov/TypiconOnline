using System;
using System.Collections.Generic;
using System.Text;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query
{
    public class DataQueryAsyncProcessor : DataQueryProcessor
    {
        public DataQueryAsyncProcessor(Container container) : base(container)
        {
        }

        public override TResult Process<TResult>(IQuery<TResult> query)
        {
            AsyncScopedLifestyle.BeginScope(Container);

            return base.Process(query);
        }
    }
}
