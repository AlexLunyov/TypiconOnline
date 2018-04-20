using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для составления Канонов с определением тропарей
    /// отдельно для каждой песни Канона
    /// </summary>
    public class KanonasCustomRule : KanonasRuleBase
    {
        public KanonasCustomRule(string name, IRuleSerializerRoot serializerRoot,
            IElementViewModelFactory<KanonasCustomRule> viewModelFactory) : base(name, serializerRoot)
        {
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException("IElementViewModelFactory in KanonasRuleBase");
        }

        #region Properties

        /// <summary>
        /// Песни канона, где каждая отдельно определяет последовательность тропарей
        /// </summary>
        public IReadOnlyList<KOdiRule> Odes { get; private set; }

        protected IElementViewModelFactory<KanonasCustomRule> ViewModelFactory { get; }

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<KanonasCustomRule>())
            {
                //находим Odes
                Odes = GetChildElements<KOdiRule>(handler);

                //находим KAfterRules
                AfterRules = GetChildElements<KAfterRule>(handler);

                handler.Execute(this);
            }
        }

        public override void CreateViewModel(IRuleHandler handler, Action<ElementViewModel> append)
        {
            ViewModelFactory.Create(new CreateViewModelRequest<KanonasCustomRule>()
            {
                Element = this,
                Handler = handler,
                AppendModelAction = append
            });
        }
    }
}
