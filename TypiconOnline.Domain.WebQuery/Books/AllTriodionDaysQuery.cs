using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        public string GetCacheKey() => $"{nameof(AllTriodionDaysQuery)}.{Language}";

        /// <summary>
        /// Реализация поиска по модели в гриде
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Expression<Func<TriodionDayGridModel, bool>>[] Search(string searchValue)
            => new Expression<Func<TriodionDayGridModel, bool>>[]
            {
                m => EF.Functions.Like(m.Name, searchValue),
                m => EF.Functions.Like(m.ShortName, searchValue),
                m => EF.Functions.Like(m.DaysFromEaster.ToString(), searchValue),
                m => EF.Functions.Like(m.IsCelebrating.ToString(), searchValue)
            };
    }
}
