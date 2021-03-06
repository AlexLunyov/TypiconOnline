﻿using System;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Books
{
    public class WeekDayAppQuery : IQuery<WeekDayApp>
    {
        public WeekDayAppQuery(DayOfWeek dayOfWeek)
        {
            DayOfWeek = dayOfWeek;
        }

        public DayOfWeek DayOfWeek { get; set; }
    }
}
