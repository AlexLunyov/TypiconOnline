using System.Collections.Generic;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class AllMenologyRulesQuery : IDataQuery<IEnumerable<MenologyRule>>
    {
        public AllMenologyRulesQuery(int typiconId)
        {
            TypiconId = typiconId;
        }

        public int TypiconId { get; }
    }
}
