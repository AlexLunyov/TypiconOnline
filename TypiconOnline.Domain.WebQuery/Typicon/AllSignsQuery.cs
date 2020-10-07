using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllSignsQuery : IGridQuery<SignGridModel>, IHasAuthorizedAccess
    {
        public AllSignsQuery(int typiconId, string language, int? exceptSignId = null)
        {
            TypiconId = typiconId;
            Language = language;
            ExceptSignId = exceptSignId;

            Key = new TypiconEntityCanEditKey(typiconId);
        }
        public int TypiconId { get; }
        public string Language { get; }
        public int? ExceptSignId { get; }

        public IAuthorizeKey Key { get; }

        public string GetCacheKey() => $"{nameof(AllSignsQuery)}.{TypiconId}.{Language}.{ExceptSignId}";

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
