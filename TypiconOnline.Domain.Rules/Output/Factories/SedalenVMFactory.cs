using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Rules.Output.Messaging;

namespace TypiconOnline.Domain.Rules.Output.Factories
{
    public class SedalenVMFactory : ApostichaVMFactory
    {
        public SedalenVMFactory(IRuleSerializerRoot serializer) : base(serializer) { }

        protected override void AppendCustomForm(CreateViewModelRequest<YmnosStructureRule> req)
        {
            InnerAppendCustomForm(req, CommonRuleConstants.SedalenRule);
        }
    }
}
