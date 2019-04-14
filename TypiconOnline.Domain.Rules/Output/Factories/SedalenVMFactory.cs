using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Rules.Output.Messaging;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Output.Factories
{
    public class SedalenVMFactory : ApostichaVMFactory
    {
        public SedalenVMFactory(IRuleSerializerRoot serializer) : base(serializer) { }

        protected override void AppendCustomForm(CreateViewModelRequest<YmnosStructureRule> req)
        {
            if ((req.Element as SedalenRule)?.Header is ItemTextHeader header)
            {
                req.AppendModelAction(new OutputElementCollection() { OutputSectionFactory.Create(header) });
            }
            else
            {
                InnerAppendCustomForm(req, CommonRuleConstants.SedalenRule);
            }
        }
    }
}
