using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllTriodionRulesWebQuery : IGridQuery<TriodionRuleGridModel>
    {
        public AllTriodionRulesWebQuery(int typiconId, string language)
        {
            TypiconId = typiconId;
            Language = language;
        }
        public int TypiconId { get; }
        public string Language { get; }

        public string GetCacheKey() => $"{nameof(TriodionRuleGridModel)}.{TypiconId}.{Language}";

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
