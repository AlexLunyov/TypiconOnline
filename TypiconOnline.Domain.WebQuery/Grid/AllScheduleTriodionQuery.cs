using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Grid
{
    public class AllScheduleTriodionQuery : IGridQuery<TriodionRuleGridModel>, IHasAuthorizedAccess
    {
        public AllScheduleTriodionQuery(int typiconId)
        {
            TypiconId = typiconId;
            Key = new TypiconEntityCanEditKey(TypiconId);
        }
        public int TypiconId { get; }

        public IAuthorizeKey Key { get; }

        public string GetCacheKey() => $"{nameof(AllScheduleTriodionQuery)}.{TypiconId}";

        /// <summary>
        /// Реализация поиска по модели в гриде
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Expression<Func<TriodionRuleGridModel, bool>>[] Search(string searchValue)
            => new Expression<Func<TriodionRuleGridModel, bool>>[]
            {
                m => EF.Functions.Like(m.Name, searchValue),
                m => EF.Functions.Like(m.DaysFromEaster.ToString(), searchValue),
                m => EF.Functions.Like(m.TemplateName, searchValue)
            };
    }
}
