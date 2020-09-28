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
    public class AllCommonRulesQuery : IGridQuery<CommonRuleGridModel>
    {
        public AllCommonRulesQuery(int typiconId)
        {
            TypiconId = typiconId;
        }
        public int TypiconId { get; }
        //public string Search { get; set; }

        public string GetKey() => $"{nameof(AllCommonRulesQuery)}.{TypiconId}";
    }
}
