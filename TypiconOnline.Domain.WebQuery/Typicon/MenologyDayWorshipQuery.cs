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
    public class MenologyDayWorshipQuery : IQuery<Result<IQueryable<MenologyDayWorshipModel>>>
    {
        public MenologyDayWorshipQuery(DateTime? date, string language)
        {
            Date = date;
            Language = language;
        }
        /// <summary>
        /// LeapDate
        /// </summary>
        public DateTime? Date { get; }
        public string Language { get; }
    }
}
