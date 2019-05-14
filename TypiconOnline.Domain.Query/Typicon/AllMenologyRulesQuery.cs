using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class AllMenologyRulesQuery : IQuery<IEnumerable<MenologyRule>>
    {
        public AllMenologyRulesQuery(int typiconId)
        {
            TypiconId = typiconId;
        }

        public int TypiconId { get; }
    }
}
