using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Books
{
    public class PsalmQuery : IDataQuery<Psalm>
    {
        public PsalmQuery(int number)
        {
            Number = number;
        }

        public int Number { get; set; }
    }
}
