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

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllExplicitAddRulesQuery : IGridQuery<ExplicitAddRuleGridModel>
    {
        public AllExplicitAddRulesQuery(int typiconId)
        {
            TypiconId = typiconId;
        }
        public int TypiconId { get; }

        public string GetCacheKey() => $"{nameof(AllExplicitAddRulesQuery)}.{TypiconId}";

        /// <summary>
        /// Реализация поиска по модели в гриде
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Expression<Func<ExplicitAddRuleGridModel, bool>>[] Search(string searchValue)
        {
            var s = $"%{searchValue}%";

            var list = new Expression<Func<ExplicitAddRuleGridModel, bool>>[]
            {
                m => EF.Functions.Like(m.Date, searchValue)
            };

            return list;
        }
    }
}
