using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllScheduleTriodionToAddQuery : IGridQuery<TriodionRuleGridModel>, IHasAuthorizedAccess
    {
        public AllScheduleTriodionToAddQuery(int typiconId, int? daysFromEaster)
        {
            TypiconId = typiconId;
            DaysFormEaster = daysFromEaster;
            Key = new TypiconEntityCanEditKey(TypiconId);
        }
        public int TypiconId { get; }
        public int? DaysFormEaster { get; }

        public IAuthorizeKey Key { get; }

        public string GetCacheKey() => $"{nameof(AllScheduleTriodionToAddQuery)}.{TypiconId}.{DaysFormEaster}";

        /// <summary>
        /// Реализация поиска по модели в гриде
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Expression<Func<TriodionRuleGridModel, bool>>[] Search(string searchValue)
        {
            var s = $"%{searchValue}%";

            var list = new Expression<Func<TriodionRuleGridModel, bool>>[]
            {
                m => EF.Functions.Like(m.Name, searchValue),
                m => EF.Functions.Like(m.DaysFromEaster.ToString(), searchValue),
                m => EF.Functions.Like(m.TemplateName, searchValue)
            };

            return list;
        }
    }
}
