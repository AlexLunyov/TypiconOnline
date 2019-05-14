using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class ModifiedRuleHighestPriorityQuery : IQuery<ModifiedRule>
    {
        public ModifiedRuleHighestPriorityQuery(int typiconVersionId, DateTime date)
        {
            TypiconVersionId = typiconVersionId;
            Date = date;
        }

        public int TypiconVersionId { get; }
        public DateTime Date { get; }
    }
}
