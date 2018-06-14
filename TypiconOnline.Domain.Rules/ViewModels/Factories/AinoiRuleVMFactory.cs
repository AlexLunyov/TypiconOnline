using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class AinoiRuleVMFactory : KekragariaRuleVMFactory
    {
        public AinoiRuleVMFactory(IRuleSerializerRoot serializer) : base(serializer) { }

        protected override void AppendCustomForm(CreateViewModelRequest<YmnosStructureRule> req)
        {
            ConstructWithCommonRule(req, CommonRuleConstants.AinoiRule);
        }
    }
}
