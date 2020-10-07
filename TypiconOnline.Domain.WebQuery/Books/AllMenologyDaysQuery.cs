using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Books
{
    public class AllMenologyDaysQuery : IGridQuery<MenologyDayGridModel>
    {
        public AllMenologyDaysQuery(string language)
        {
            Language = language;
        }
        public string Language { get; }

        public string GetCacheKey() => $"{nameof(AllMenologyDaysQuery)}.{Language}";

        /// <summary>
        /// Реализация поиска по модели в гриде
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Expression<Func<MenologyDayGridModel, bool>>[] Search(string searchValue)
        {
            var s = $"%{searchValue}%";

            var list = new Expression<Func<MenologyDayGridModel, bool>>[]
            {
                m => EF.Functions.Like(m.Name, searchValue),
                m => EF.Functions.Like(m.ShortName, searchValue),
                m => EF.Functions.Like(m.Date, searchValue),
                m => EF.Functions.Like(m.LeapDate, searchValue),
                m => EF.Functions.Like(m.IsCelebrating.ToString(), searchValue)
            };

            return list;
        }
    }
}
