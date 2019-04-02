using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Output.Factories;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class AinoiRuleSerializer : KekragariaRuleSerializer, IRuleSerializer<AinoiRule>
    {
        public AinoiRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.AinoiRuleNode };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new AinoiRule(new AinoiRuleVMFactory(SerializerRoot), req.Descriptor.GetElementName());
        }
    }
}
