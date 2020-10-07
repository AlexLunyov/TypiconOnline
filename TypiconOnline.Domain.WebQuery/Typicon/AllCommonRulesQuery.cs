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
    public class AllCommonRulesQuery : IGridQuery<CommonRuleGridModel>
    {
        public AllCommonRulesQuery(int typiconId)
        {
            TypiconId = typiconId;
        }
        public int TypiconId { get; }

        public string GetCacheKey() => $"{nameof(AllCommonRulesQuery)}.{TypiconId}";

        /// <summary>
        /// Реализация поиска по модели в гриде
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Expression<Func<CommonRuleGridModel, bool>>[] Search(string searchValue)
        {
            var s = $"%{searchValue}%";

            var list = new Expression<Func<CommonRuleGridModel, bool>>[]
            {
                m => EF.Functions.Like(m.Name, searchValue)
            };

            return list;
        }
    }
}
