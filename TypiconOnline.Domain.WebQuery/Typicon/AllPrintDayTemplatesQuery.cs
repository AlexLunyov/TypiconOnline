using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public string GetCacheKey() => $"{nameof(AllPrintDayTemplatesQuery)}.{TypiconId}.{ForDraft}";

        /// <summary>
        /// Реализация поиска по модели в гриде
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Expression<Func<PrintDayTemplateGridModel, bool>>[] Search(string searchValue)
            => new Expression<Func<PrintDayTemplateGridModel, bool>>[]
            {
                m => EF.Functions.Like(m.Name, searchValue),
                m => EF.Functions.Like(m.Number.ToString(), searchValue)
            };
    }
}
