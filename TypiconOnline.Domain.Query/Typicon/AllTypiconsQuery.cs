using System.Collections.Generic;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class AllTypiconsQuery : IDataQuery<IEnumerable<TypiconVersionDTO>>
    {
        public AllTypiconsQuery() { }
    }
}
