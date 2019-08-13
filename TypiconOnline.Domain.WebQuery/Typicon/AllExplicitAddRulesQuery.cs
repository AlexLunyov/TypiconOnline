using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllExplicitAddRulesQuery : IGridQuery<ExplicitAddRuleModel>
    {
        public AllExplicitAddRulesQuery(int typiconId)
        {
            TypiconId = typiconId;
        }
        public int TypiconId { get; }

        public string GetKey() => $"{nameof(AllExplicitAddRulesQuery)}.{TypiconId}";
    }
}
