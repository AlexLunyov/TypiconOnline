using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.ViewModels.Factories;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class TroparionRuleSerializer : YmnosStructureRuleSerializer, IRuleSerializer<TroparionRule>
    {
        public TroparionRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.TroparionRuleNode };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new TroparionRule(new TroparionRuleVMFactory(SerializerRoot), req.Descriptor.GetElementName());
        }
    }
}