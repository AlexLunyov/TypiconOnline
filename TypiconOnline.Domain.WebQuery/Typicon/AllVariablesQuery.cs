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
    public class AllVariablesQuery : IGridQuery<TypiconVariableModel>
    {
        public AllVariablesQuery(int typiconId)
        {
            TypiconId = typiconId;
        }
        public int TypiconId { get; }
        //public string Search { get; set; }
        public string GetCacheKey() => $"{nameof(AllVariablesQuery)}.{TypiconId}";

        /// <summary>
        /// Реализация поиска по модели в гриде
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Expression<Func<TypiconVariableModel, bool>>[] Search(string searchValue)
            => new Expression<Func<TypiconVariableModel, bool>>[]
            {
                m => EF.Functions.Like(m.Name, searchValue),
                m => EF.Functions.Like(m.Type.ToString(), searchValue),
                m => EF.Functions.Like(m.Count.ToString(), searchValue)
            };
    }
}
