using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.Rules.Interfaces
{
    public interface IElementViewModelFactory<T> where T : RuleElement, IViewModelElement
    {
        void Create(CreateViewModelRequest<T> req);
    }
}
