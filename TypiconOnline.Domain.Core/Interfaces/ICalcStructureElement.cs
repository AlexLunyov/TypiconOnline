using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для элементов правил, которые возвращают тексты богослужений
    /// </summary>
    public interface ICalcStructureElement
    {
        DayElementBase Calculate(RuleHandlerSettings settings);
    }
}
