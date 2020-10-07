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
    public class AllOktoikhDaysQuery : IGridQuery<OktoikhDayGridModel>
    {
        public AllOktoikhDaysQuery(/*string language*/)
        {
            //Language = language;
        }
        //public string Language { get; }

        public string GetCacheKey() => nameof(AllOktoikhDaysQuery);

        /// <summary>
        /// Реализация поиска по модели в гриде
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Expression<Func<OktoikhDayGridModel, bool>>[] Search(string searchValue)
            => new Expression<Func<OktoikhDayGridModel, bool>>[]
            {
                m => EF.Functions.Like(m.Ihos.ToString(), searchValue),
                m => EF.Functions.Like(m.DayOfWeek, searchValue),
            };
    }
}
