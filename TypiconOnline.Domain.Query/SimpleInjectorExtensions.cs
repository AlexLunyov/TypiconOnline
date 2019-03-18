﻿using System;
using System.Collections.Generic;
using System.Text;
using SimpleInjector;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query
{
    public static class SimpleInjectorExtensions
    {
        public static Container RegisterTypiconQueryClasses(this Container container)
        {
            //container.Register(typeof(IDataQuery<>), typeof(QueryProcessor).Assembly);
            container.Register(typeof(IDataQueryHandler<,>), typeof(DbContextQueryBase).Assembly);
            container.Register<IDataQueryProcessor, DataQueryProcessor>();

            //container.Register(typeof(IQuery<>), typeof(QueryProcessor).Assembly);
            container.Register(typeof(IQueryHandler<,>), typeof(DbContextQueryBase).Assembly);
            container.Register<IQueryProcessor, QueryProcessor>();

            return container;
        }
    }
}
