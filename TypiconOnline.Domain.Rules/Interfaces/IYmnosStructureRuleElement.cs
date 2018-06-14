using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Rules.Interfaces;

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
