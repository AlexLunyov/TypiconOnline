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
    public class AllOktoikhDaysQuery : IGridQuery<OktoikhDayGridModel>
    {
        public AllOktoikhDaysQuery(/*string language*/)
        {
            //Language = language;
        }
        //public string Language { get; }

        public string GetKey() => nameof(AllOktoikhDaysQuery);
        //public string GetKey() => $"{nameof(AllOktoikhDaysQuery)}.{Language}";
    }
}
