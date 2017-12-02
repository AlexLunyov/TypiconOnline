using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.ViewModels.Messaging
{
    public class CreateViewModelRequest<T> where T : RuleElement, IViewModelElement
    {
        public T Element { get; set; }
        public IRuleHandler Handler { get; set; }
        public Action<ElementViewModel> AppendModelAction { get; set; }
    }
}
