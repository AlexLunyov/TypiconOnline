using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Books
{
    public class CurrentEasterQuery : IDataQuery<DateTime>
    {
        public CurrentEasterQuery(int year)
        {
            Year = year;
        }

        public int Year { get; }
    }
}
