using System.Collections.Generic;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для сериализации элементов Правил
    /// </summary>
    public interface IRuleSerializer
    {
        IEnumerable<string> ElementNames { get; }
        //RuleElement Create(string description);
        IRuleElement Deserialize(IDescriptor descriptor, IAsAdditionElement parent);
        string Serialize(IRuleElement element);
    }
}
