using System;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class DayRuleFromTriodionQuery : IDataQuery<DayRuleDto>
    {
        public DayRuleFromTriodionQuery(int typiconId, DateTime date)
        {
            TypiconId = typiconId;
            Date = date;
        }

        public int TypiconId { get; }
        public DateTime Date { get; }
    }
}
