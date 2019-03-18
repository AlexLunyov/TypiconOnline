using System;
using System.Collections.Generic;
using System.Text;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query
{
    public class QueryAsyncProcessor : QueryProcessor
    {
        public QueryAsyncProcessor(Container container) : base(container)
        {
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var dbContext = container.GetInstance<TypiconDBContext>();

                container.RegisterInstance(dbContext);
            }
        }
    }
}
