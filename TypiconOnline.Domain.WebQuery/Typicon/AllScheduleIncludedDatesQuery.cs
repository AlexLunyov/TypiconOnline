using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllScheduleIncludedDatesQuery : IGridQuery<DateGridItem>, IHasAuthorizedAccess
    {
        public AllScheduleIncludedDatesQuery(int typiconId)
        {
            TypiconId = typiconId;
            Key = new TypiconEntityCanEditKey(TypiconId);
        }
        public int TypiconId { get; }

        public IAuthorizeKey Key { get; }

        public string GetCacheKey() => $"{nameof(AllScheduleIncludedDatesQuery)}.{TypiconId}";

        /// <summary>
        /// Реализация поиска по модели в гриде
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Expression<Func<DateGridItem, bool>>[] Search(string searchValue)
        {
            var s = $"%{searchValue}%";

            var list = new Expression<Func<DateGridItem, bool>>[]
            {
                m => EF.Functions.Like(m.Date.ToString(CommonConstants.DateFormat), searchValue)
            };

            return list;
        }
    }
}
