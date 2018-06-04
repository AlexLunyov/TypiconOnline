using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для дочерних элементов в Правиле, составляющем последовательность песнопений для эксапостилария
    /// </summary>
    public interface IExapostilarionRuleElement : IStructureRuleChildElement<Exapostilarion>
    {
    }
}
