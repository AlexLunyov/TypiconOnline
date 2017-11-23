﻿using System.Collections.Generic;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Interfaces
{
    interface IRuleFactory
    {
        IEnumerable<string> ElementNames { get; }
        RuleElement Create(string description);
    }
}
