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
    public class AllPrintDayTemplatesQuery : IGridQuery<PrintDayTemplateGridModel>
    {
        public AllPrintDayTemplatesQuery(int typiconId, bool forDraft = true)
        {
            TypiconId = typiconId;
            ForDraft = forDraft;
        }
        public int TypiconId { get; }

        public bool ForDraft { get; set; }
        //public string Search { get; set; }

        public string GetKey() => $"{nameof(AllPrintDayTemplatesQuery)}.{TypiconId}.{ForDraft}";
    }
}
