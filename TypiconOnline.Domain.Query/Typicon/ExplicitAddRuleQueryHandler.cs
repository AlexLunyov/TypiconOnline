using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{

    public class ExplicitAddRuleQueryHandler : DbContextQueryBase, IDataQueryHandler<ExplicitAddRuleQuery, ExplicitAddRule>
    {
        public ExplicitAddRuleQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public ExplicitAddRule Handle([NotNull] ExplicitAddRuleQuery query)
        {
            return DbContext.Set<ExplicitAddRule>()
                .FirstOrDefault(c => c.TypiconVersionId == query.TypiconVersionId && c.Date.Date == query.Date);
        }
    }
}
