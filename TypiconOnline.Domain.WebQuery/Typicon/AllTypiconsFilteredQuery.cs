using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllTypiconsFilteredQuery : IGridQuery<TypiconEntityFilteredModel>
    {
        public AllTypiconsFilteredQuery() { }

        public AllTypiconsFilteredQuery(int userId, string language)
        {
            UserId = userId;
            Language = language;
        }

        public int UserId { get; set; }

        public string Language { get; } = "cs-ru";
        //public string Search { get; set; }

        public string GetCacheKey() => $"{nameof(AllTypiconsFilteredQuery)}.{UserId}.{Language}";

        /// <summary>
        /// Реализация поиска по модели в гриде
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Expression<Func<TypiconEntityFilteredModel, bool>>[] Search(string searchValue)
        {
            var s = $"%{searchValue}%";

            var list = new Expression<Func<TypiconEntityFilteredModel, bool>>[]
            {
                m => EF.Functions.Like(m.Name, searchValue),
                m => EF.Functions.Like(m.SystemName.ToString(), searchValue)
            };

            return list;
        }
    }
}
