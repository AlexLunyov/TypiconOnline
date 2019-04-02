using System;
using TypiconOnline.Domain.Rules.Output;

namespace TypiconOnline.Domain.Rules.Interfaces
{
    public interface IViewModelElement 
    {
        void CreateViewModel(IRuleHandler handler, Action<OutputElementCollection> append);
    }
}
