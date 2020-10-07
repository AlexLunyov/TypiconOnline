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
    public class AllScheduleSignsToLoadQuery : IGridQuery<SignGridModel>, IHasAuthorizedAccess
    {
        public AllScheduleSignsToLoadQuery(int typiconId)
        {
            TypiconId = typiconId;
            Key = new TypiconEntityCanEditKey(TypiconId);
        }
        public int TypiconId { get; }

        public IAuthorizeKey Key { get; }

        //public string Search { get; set; }

        public string GetCacheKey() => $"{nameof(AllScheduleSignsToLoadQuery)}.{TypiconId}";

        /// <summary>
        /// Реализация поиска по модели в гриде
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Expression<Func<SignGridModel, bool>>[] Search(string searchValue)
            => new Expression<Func<SignGridModel, bool>>[]
            {
                m => EF.Functions.Like(m.Name, searchValue),
                m => EF.Functions.Like(m.Number.ToString(), searchValue),
                m => EF.Functions.Like(m.Priority.ToString(), searchValue),
                m => EF.Functions.Like(m.TemplateName, searchValue)
            };
    }
}
