using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface ITypiconFacade
    {
        MenologyRule GetMenologyRule(int typiconId, DateTime date);
        IEnumerable<MenologyRule> GetAllMenologyRules(int typiconId);
        TriodionRule GetTriodionRule(int typiconId, DateTime date);
        IEnumerable<TriodionRule> GetAllTriodionRules(int typiconId);
        ModifiedRule GetModifiedRuleHighestPriority(int typiconId, DateTime date);
        DateTime GetCurrentEaster(int year);
    }
}
