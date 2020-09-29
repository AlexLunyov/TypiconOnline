using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Context
{
    public abstract class WebDbContextQueryBase
    {
        protected WebDbContext DbContext { get; }

        protected WebDbContextQueryBase(WebDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
