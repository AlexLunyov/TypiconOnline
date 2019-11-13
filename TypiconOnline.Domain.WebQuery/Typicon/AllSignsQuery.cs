using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllSignsQuery : IGridQuery<SignGridModel>
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

        public string GetKey() => $"{nameof(AllSignsQuery)}.{TypiconId}.{Language}.{ExceptSignId}";
    }
}
