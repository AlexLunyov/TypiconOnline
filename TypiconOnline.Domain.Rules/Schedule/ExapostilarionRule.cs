using System;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Rules.Output.Messaging;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class ExapostilarionRule : StructureRuleBase<Exapostilarion, IExapostilarionRuleElement>, IViewModelElement
    {
        public ExapostilarionRule(IElementViewModelFactory<ExapostilarionRule> viewModelFactory, string name) : base(name)
        {
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException("IElementViewModelFactory in ExapostilarionRule");
        }

        public IElementViewModelFactory<ExapostilarionRule> ViewModelFactory { get; }


        public void CreateViewModel(IRuleHandler handler, Action<OutputElementCollection> append)
        {
            ViewModelFactory.Create(new CreateViewModelRequest<ExapostilarionRule>()
            {
                Element = this,
                Handler = handler,
                AppendModelAction = append
            });
        }

        protected override bool IsAuthorized(IRuleHandler handler) => handler.IsAuthorized<ExapostilarionRule>();
    }
}
