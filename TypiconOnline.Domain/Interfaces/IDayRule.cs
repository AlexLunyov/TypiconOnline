using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс используется для обобщения DayRule и ModifiedRule
    /// </summary>
    public interface IDayRule
    {
        List<DayWorship> DayWorships { get; }
        //bool IsAddition { get; set; }
        //Sign Template { get; }
        //string RuleDefinition { get; }
    }
}
