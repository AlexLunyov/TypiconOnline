using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Books
{
    public class OktoikhDayQuery : IQuery<OktoikhDay>
    {
        public OktoikhDayQuery(DateTime date)
        {
            Date = date;
        }
        public DateTime Date { get; }
    }
}
