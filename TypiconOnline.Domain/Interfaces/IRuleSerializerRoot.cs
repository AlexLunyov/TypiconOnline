using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IRuleSerializerRoot
    {
        BookStorage BookStorage { get; }
        RuleSerializerContainerBase<T> Container<T>() where T : RuleElement;
        RuleSerializerContainerBase<T> Container<T, U>() where T : RuleElement;
    }
}
