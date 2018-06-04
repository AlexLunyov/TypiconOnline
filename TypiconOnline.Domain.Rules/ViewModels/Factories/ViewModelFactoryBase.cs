using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.ViewModels.Messaging;

namespace TypiconOnline.Domain.Rules.ViewModels.Factories
{
    public abstract class ViewModelFactoryBase<T> : IElementViewModelFactory<T> where T : RuleElement, IViewModelElement
    {
        public IRuleSerializerRoot Serializer { get; }

        public ViewModelFactoryBase(IRuleSerializerRoot serializer)
        {
            Serializer = serializer ?? throw new ArgumentNullException("IRuleSerializerRoot");
        }

        public abstract void Create(CreateViewModelRequest<T> req);
    }
}
