using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.ViewModels
{
    public class AinoiRuleViewModel : KekragariaRuleViewModel
    {
        public AinoiRuleViewModel(YmnosStructureRule rule, IRuleHandler handler) : base(rule, handler) { }

        protected override void ConstructForm(IRuleHandler handler)
        {
            ConstructWithCommonRule(handler, CommonRuleConstants.AinoiRule);
        }
    }
}
