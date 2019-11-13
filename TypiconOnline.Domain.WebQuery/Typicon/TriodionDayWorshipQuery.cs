using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class TriodionDayWorshipQuery : IQuery<Result<IQueryable<TriodionDayWorshipModel>>>
    {
        public TriodionDayWorshipQuery(int daysFromEaster, string language)
        {
            DaysFromEaster = daysFromEaster;
            Language = language;
        }
        /// <summary>
        /// DaysFromEaster
        /// </summary>
        public int DaysFromEaster { get; }
        public string Language { get; }
    }
}
