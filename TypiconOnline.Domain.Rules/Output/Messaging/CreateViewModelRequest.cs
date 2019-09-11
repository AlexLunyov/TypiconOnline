using System;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Output.Messaging
{
    public class CreateViewModelRequest<T> where T : RuleElementBase, IViewModelElement
    {
        public T Element { get; set; }
        public DateTime Date { get; set; }
        public IRuleHandler Handler { get; set; }
        public Action<OutputSectionModelCollection> AppendModelAction { get; set; }
    }
}
