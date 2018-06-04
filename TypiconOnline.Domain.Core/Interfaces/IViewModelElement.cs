using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Core.Interfaces
{
    public interface IViewModelElement 
    {
        void CreateViewModel(IRuleHandler handler, Action<ElementViewModel> append);
    }
}
