using System.Collections.Generic;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllTypiconsQuery : IQuery<IEnumerable<TypiconEntityModel>>
    {
        public AllTypiconsQuery() { }

        public AllTypiconsQuery(string language)
        {
            Language = language;
        }

        public string Language { get; } = "cs-ru";
    }
}
