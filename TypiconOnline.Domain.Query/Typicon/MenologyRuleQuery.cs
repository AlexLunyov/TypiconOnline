using System;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class MenologyRuleQuery : IQuery<MenologyRule>
    {
        public MenologyRuleQuery(int typiconVersionId, DateTime date)
        {
            TypiconVersionId = typiconVersionId;
            Date = date;
        }

        public int TypiconVersionId { get; }
        public DateTime Date { get; }
    }
}
