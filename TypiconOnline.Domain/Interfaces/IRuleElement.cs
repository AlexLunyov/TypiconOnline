using System.Collections.Generic;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IRuleElement
    {
        string ElementName { get; }
        bool IsValid { get; }
        IReadOnlyCollection<BusinessConstraint> GetBrokenConstraints();
    }
}
