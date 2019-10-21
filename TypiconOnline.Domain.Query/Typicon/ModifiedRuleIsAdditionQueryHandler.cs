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

    public class ModifiedRuleIsAdditionQueryHandler : DbContextQueryBase, IQueryHandler<ModifiedRuleIsAdditionQuery, ModifiedRule>
    {
        public ModifiedRuleIsAdditionQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public ModifiedRule Handle([NotNull] ModifiedRuleIsAdditionQuery query)
        {
            var found = DbContext.Set<ModifiedRule>()
                .Where(c => c.Parent.TypiconVersionId == query.TypiconVersionId 
                        && c.Date.Date == query.Date
                        && c.IsAddition)
                .ToList();

            return (found != null) ? found.Min() : default;
        }
    }
}
