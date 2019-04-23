using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllTypiconsFilteredQuery : IDataQuery<Result<IQueryable<TypiconEntityFilteredModel>>>
    {
        public AllTypiconsFilteredQuery() { }

        public AllTypiconsFilteredQuery(int userId, string language)
        {
            UserId = userId;
            Language = language;
        }

        public int UserId { get; set; }

        public string Language { get; } = "cs-ru";
    }
}
