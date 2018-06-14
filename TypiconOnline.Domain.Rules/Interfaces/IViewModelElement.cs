using System;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Rules.Interfaces
{
    public interface IViewModelElement 
    {
        void CreateViewModel(IRuleHandler handler, Action<ElementViewModel> append);
    }
}
