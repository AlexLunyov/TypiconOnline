using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class EktenisRuleSerializer : ExecContainerSerializer, IRuleSerializer<EktenisRule>
    {
        public EktenisRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.EktenisNode };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req) => new EktenisRule(req.Descriptor.GetElementName());
    }
}
