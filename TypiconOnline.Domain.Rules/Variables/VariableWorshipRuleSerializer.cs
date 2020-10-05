using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Variables
{
    public class VariableWorshipRuleSerializer : ExecContainerSerializer, IRuleSerializer<VariableWorshipRule>
    {
        public VariableWorshipRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.WorshipRuleVariableNode };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req) => new VariableWorshipRule(SerializerRoot);

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            (req.Element as VariableWorshipRule).VariableName = req.Descriptor.Element.Attributes[RuleConstants.WorshipRuleVariableNameAttr]?.Value;
        }
    }
}
