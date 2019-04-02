using TypiconOnline.Domain.Rules.Output.Messaging;

namespace TypiconOnline.Domain.Rules.Interfaces
{
    public interface IElementViewModelFactory<T> where T : RuleElementBase, IViewModelElement
    {
        void Create(CreateViewModelRequest<T> req);
    }
}
