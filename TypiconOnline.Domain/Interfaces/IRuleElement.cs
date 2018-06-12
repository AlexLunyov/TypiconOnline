using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
