using System.Collections.Generic;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class AllTypiconsQuery : IDataQuery<IEnumerable<TypiconDTO>>
    {
        public AllTypiconsQuery() { }

        public AllTypiconsQuery(string language)
        {
            Language = language;
        }

        public string Language { get; } = "cs-ru";
    }
}
