using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public abstract class ViewModelFactoryBase<T> : IElementViewModelFactory<T> where T : RuleElementBase, IViewModelElement
    {
        public IRuleSerializerRoot Serializer { get; }

        public ViewModelFactoryBase(IRuleSerializerRoot serializer)
        {
            Serializer = serializer ?? throw new ArgumentNullException("IRuleSerializerRoot");
        }

        public abstract void Create(CreateViewModelRequest<T> req);
    }
}
