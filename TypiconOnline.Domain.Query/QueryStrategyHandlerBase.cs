﻿using JetBrains.Annotations;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query
{
    public abstract class QueryStrategyHandlerBase : DbContextQueryBase
    {
        protected IQueryProcessor QueryProcessor { get; }

        protected QueryStrategyHandlerBase(TypiconDBContext dbContext, [NotNull] IQueryProcessor queryProcessor) : base(dbContext)
        {
            QueryProcessor = queryProcessor;
        }
    }
}
