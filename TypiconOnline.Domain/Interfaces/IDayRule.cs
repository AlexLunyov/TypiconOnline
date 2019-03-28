using System.Collections.Generic;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс используется для обобщения DayRule и ModifiedRule
    /// </summary>
    public interface IDayRule
    {
        IReadOnlyList<DayWorship> DayWorships { get; }
        //bool IsAddition { get; set; }
        //Sign Template { get; }
        //string RuleDefinition { get; }
    }
}
