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
        RuleElement Deserialize(IDescriptor descriptor, IRewritableElement parent);
        string Serialize(RuleElement element);
    }
}
