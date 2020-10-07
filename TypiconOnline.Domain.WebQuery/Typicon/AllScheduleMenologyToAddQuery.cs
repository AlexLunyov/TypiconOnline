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

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllScheduleMenologyToAddQuery : IGridQuery<MenologyRuleGridModel>, IHasAuthorizedAccess
    {
        public AllScheduleMenologyToAddQuery(int typiconId, DateTime? date)
        {
            TypiconId = typiconId;
            Date = date;
            Key = new TypiconEntityCanEditKey(TypiconId);
        }
        public int TypiconId { get; }
        public DateTime? Date { get; }

        public IAuthorizeKey Key { get; }

        //public string Search { get; set; }

        public string GetCacheKey() => $"{nameof(AllScheduleMenologyToAddQuery)}.{TypiconId}";

        /// <summary>
        /// Реализация поиска по модели в гриде
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Expression<Func<MenologyRuleGridModel, bool>>[] Search(string searchValue)
        {
            var s = $"%{searchValue}%";

            var list = new Expression<Func<MenologyRuleGridModel, bool>>[]
            {
                m => EF.Functions.Like(m.Name, searchValue),
                m => EF.Functions.Like(m.TemplateName, searchValue),
                m => EF.Functions.Like(m.Date, searchValue),
                m => EF.Functions.Like(m.LeapDate, searchValue)
            };

            return list;
        }
    }
}
