using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Output.Factories;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class ApostichaRuleSerializer : YmnosStructureRuleSerializer, IRuleSerializer<ApostichaRule>
    {
        public ApostichaRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.ApostichaRuleNode,
                RuleConstants.LitiRuleNode };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new ApostichaRule(new ApostichaVMFactory(SerializerRoot), req.Descriptor.GetElementName());
        }
    }
}
