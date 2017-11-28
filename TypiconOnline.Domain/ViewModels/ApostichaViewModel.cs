using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.ViewModels
{
    public class ApostichaViewModel : YmnosStructureViewModel
    {
        public ApostichaViewModel(YmnosStructureRule rule, IRuleHandler handler) : base(rule, handler) { }

        protected override void ConstructForm(IRuleHandler handler)
        {
            TextHolder header = handler.Settings.Rule.Owner.GetCommonRuleChildren(
                new CommonRuleServiceRequest() { Key = CommonRuleConstants.ApostichaRule, RuleSerializer = Serializer })
                .FirstOrDefault() as TextHolder;

            _childElements.Add(new TextHolderViewModel(header, handler));
        }
    }
}
