using System;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Books
{
    public class TheotokionAppQuery : IDataQuery<YmnosGroup>
    {
        public TheotokionAppQuery(TheotokionAppPlace place, int ihos, DayOfWeek dayOfWeek)
        {
            Place = place;
            Ihos = ihos;
            DayOfWeek = dayOfWeek;
        }

        public TheotokionAppPlace Place { get; }
        public int Ihos { get; }
        public DayOfWeek DayOfWeek { get; }
    }
}
