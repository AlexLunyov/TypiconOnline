using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllSignsQuery : IDataQuery<Result<IQueryable<SignModel>>>
    {
        public AllSignsQuery(int typiconId, string language, int? exceptSignId = null)
        {
            TypiconId = typiconId;
            Language = language;
            ExceptSignId = exceptSignId;
        }
        public int TypiconId { get; }
        public string Language { get; }
        public int? ExceptSignId { get; }
    }
}
