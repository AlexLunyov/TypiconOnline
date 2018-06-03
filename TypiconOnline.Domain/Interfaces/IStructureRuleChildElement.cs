﻿using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IStructureRuleChildElement<T> where T : DayElementBase
    {
        T GetStructure(RuleHandlerSettings settings);
    }
}