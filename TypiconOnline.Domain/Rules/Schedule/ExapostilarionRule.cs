using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class ExapostilarionRule : StructureRuleBase<Exapostilarion, IExapostilarionRuleElement>, IViewModelElement
    {
        public ExapostilarionRule(IElementViewModelFactory<ExapostilarionRule> viewModelFactory, string name) : base(name)
        {
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException("IElementViewModelFactory in ExapostilarionRule");
        }

        public IElementViewModelFactory<ExapostilarionRule> ViewModelFactory { get; }


        public void CreateViewModel(IRuleHandler handler, Action<ElementViewModel> append)
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
