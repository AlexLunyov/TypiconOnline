using System.Collections.Generic;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllTypiconsQuery : IQuery<IEnumerable<TypiconEntityModel>>
    {
        public AllTypiconsQuery(bool withTemplates = false, string language = "cs-ru") 
        {
            WithTemplates = withTemplates;
        }

        public string Language { get; }
        public bool WithTemplates { get; }
    }
}
