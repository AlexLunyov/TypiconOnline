using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Interfaces
{
    public interface IStructureRuleChildElement<out T> where T : DayElementBase
    {
        T GetStructure(RuleHandlerSettings settings);
    }
}