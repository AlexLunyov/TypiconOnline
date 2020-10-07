using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Context;

namespace TypiconOnline.Domain.WebQuery
{
    public abstract class WebDbContextQueryBase
    {
        protected WebDbContext DbContext { get; }

        protected WebDbContextQueryBase(WebDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
