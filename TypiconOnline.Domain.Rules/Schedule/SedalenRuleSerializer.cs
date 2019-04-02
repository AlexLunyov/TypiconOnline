using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Output.Factories;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class SedalenRuleSerializer : YmnosStructureRuleSerializer, IRuleSerializer<SedalenRule>
    {
        public SedalenRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.SedalenRuleNode };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new SedalenRule(new SedalenVMFactory(SerializerRoot), req.Descriptor.GetElementName());
        }
    }
}
