using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IViewModelElement
    {
        ElementViewModel CreateViewModel(IRuleHandler handler);
    }
}
