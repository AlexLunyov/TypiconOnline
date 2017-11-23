using System.Collections.Generic;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IRuleFactory<T> where T: RuleElement
    {
        IEnumerable<string> ElementNames { get; }
        T Create(IDescriptor descriptor);
    }
}
