using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Interfaces
{
    /// <summary>
    /// Интерфейс для элементов правил, которые возвращают тексты богослужений
    /// </summary>
    public interface ICalcStructureElement
    {
        DayElementBase Calculate(RuleHandlerSettings settings);
    }
}
