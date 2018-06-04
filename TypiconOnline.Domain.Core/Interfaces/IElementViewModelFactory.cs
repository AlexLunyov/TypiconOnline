using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.Core.Interfaces
{
    public interface IElementViewModelFactory<T> where T : RuleElement, IViewModelElement
    {
        void Create(CreateViewModelRequest<T> req);
    }
}
