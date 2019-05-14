using System;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Books
{
    public class CurrentEasterQuery : IQuery<DateTime>
    {
        public CurrentEasterQuery(int year)
        {
            Year = year;
        }

        public int Year { get; }
    }
}
