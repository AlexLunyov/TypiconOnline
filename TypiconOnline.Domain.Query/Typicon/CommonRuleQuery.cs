﻿using JetBrains.Annotations;
using System;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class CommonRuleQuery : IDataQuery<CommonRule>
    {
        public CommonRuleQuery(int typiconId, string name)
        {
            TypiconId = typiconId;
            Name = Name;
        }

        public int TypiconId { get; }
        public string Name{ get; }
    }
}
