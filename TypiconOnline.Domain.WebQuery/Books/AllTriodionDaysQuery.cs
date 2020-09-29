using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Books
{
    public class AllTriodionDaysQuery : IGridQuery<TriodionDayGridModel>
    {
        public AllTriodionDaysQuery(string language)
        {
            Language = language;
        }
        public string Language { get; }

        //public string Search { get; set; }

        public string GetKey() => $"{nameof(AllTriodionDaysQuery)}.{Language}";
    }
}
