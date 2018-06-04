using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Rules.ViewModels.Messaging;

namespace TypiconOnline.Domain.Rules.ViewModels.Factories
{
    public class TroparionRuleVMFactory : ApostichaVMFactory
    {
        public TroparionRuleVMFactory(IRuleSerializerRoot serializer) : base(serializer) { }

        protected override void AppendCustomForm(CreateViewModelRequest<YmnosStructureRule> req)
        {
            InnerAppendCustomForm(req, CommonRuleConstants.TroparionRule);
        }
    }
}
