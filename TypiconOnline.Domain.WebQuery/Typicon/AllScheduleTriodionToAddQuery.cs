using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllScheduleTriodionToAddQuery : IGridQuery<TriodionRuleGridModel>, IHasAuthorizedAccess
    {
        public AllScheduleTriodionToAddQuery(int typiconId, int? daysFromEaster)
        {
            TypiconId = typiconId;
            DaysFormEaster = daysFromEaster;
            Key = new TypiconEntityCanEditKey(TypiconId);
        }
        public int TypiconId { get; }
        public int? DaysFormEaster { get; }

        public IAuthorizeKey Key { get; }

        //public string Search { get; set; }

        public string GetKey() => $"{nameof(AllScheduleTriodionToAddQuery)}.{TypiconId}.{DaysFormEaster}";
    }
}
