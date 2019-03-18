using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{

    public class ModifiedRuleHighestPriorityQueryHandler : DbContextQueryBase, IDataQueryHandler<ModifiedRuleHighestPriorityQuery, ModifiedRule>
    {
        public ModifiedRuleHighestPriorityQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public ModifiedRule Handle([NotNull] ModifiedRuleHighestPriorityQuery query)
        {
            var found = DbContext.Set<ModifiedRule>()
                .Where(c => c.Parent.TypiconVersionId == query.TypiconVersionId && c.Date.Date == query.Date)
                .ToList();

            return (found != null) ? found.Min() : default(ModifiedRule);
        }
    }
}
