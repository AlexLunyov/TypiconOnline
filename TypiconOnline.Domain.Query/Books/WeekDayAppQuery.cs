using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Books
{
    public class WeekDayAppQuery : IDataQuery<WeekDayApp>
    {
        public WeekDayAppQuery(DayOfWeek dayOfWeek)
        {
            DayOfWeek = dayOfWeek;
        }

        public DayOfWeek DayOfWeek { get; set; }
    }
}
