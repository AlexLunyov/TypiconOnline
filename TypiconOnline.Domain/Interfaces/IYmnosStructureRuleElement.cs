using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для дочерних элементов в Правиле, составляющем последовательность стихир
    /// </summary>
    public interface IYmnosStructureRuleElement : IStructureRuleChildElement<YmnosStructure>
    {
        
        //YmnosStructure GetStructure(RuleHandlerSettings settings);
    }
}
