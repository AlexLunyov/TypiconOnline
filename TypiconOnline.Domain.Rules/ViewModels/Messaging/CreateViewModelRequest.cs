using System;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.ViewModels.Messaging
{
    public class CreateViewModelRequest<T> where T : RuleElement, IViewModelElement
    {
        public T Element { get; set; }
        public DateTime Date { get; set; }
        public IRuleHandler Handler { get; set; }
        public Action<ElementViewModelCollection> AppendModelAction { get; set; }
    }
}
