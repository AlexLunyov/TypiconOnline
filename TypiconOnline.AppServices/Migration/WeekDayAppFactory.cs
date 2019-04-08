using System;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.WeekDayApp;

namespace TypiconOnline.AppServices.Migration
{
    public class WeekDayAppFactory : IWeekDayAppFactory
    {
        public WeekDayApp Create(string name, string definition)
        {
            return new WeekDayApp()
            {
                DayOfWeek = (DayOfWeek) Enum.Parse(typeof(DayOfWeek), name, true),
                Definition = definition
            };
        }

    }
}
