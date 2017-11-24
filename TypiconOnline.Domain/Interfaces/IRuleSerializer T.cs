using System.Collections.Generic;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Наследник IRuleSerialize. Generic параметр используется лишь для фильтрации в контейнере
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRuleSerializer<T> : IRuleSerializer where T: RuleElement
    {
        //IEnumerable<string> ElementNames { get; }
        //T Create(IDescriptor descriptor);
        //string Serialize(T element);
    }
}
