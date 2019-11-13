﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllTriodionRulesWebQuery : IGridQuery<TriodionRuleGridModel>
    {
        public AllTriodionRulesWebQuery(int typiconId, string language)
        {
            TypiconId = typiconId;
            Language = language;
        }
        public int TypiconId { get; }
        public string Language { get; }

        public string GetKey() => $"{nameof(TriodionRuleGridModel)}.{TypiconId}.{Language}";
    }
}
