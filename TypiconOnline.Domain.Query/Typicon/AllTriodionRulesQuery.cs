using System.Collections.Generic;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class AllTriodionRulesQuery : IDataQuery<IEnumerable<TriodionRule>>
    {
        public AllTriodionRulesQuery(int typiconId)
        {
            TypiconId = typiconId;
        }

        public int TypiconId { get; }
    }
}
