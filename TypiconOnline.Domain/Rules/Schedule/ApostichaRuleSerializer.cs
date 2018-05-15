using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.ViewModels.Factories;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class ApostichaRuleSerializer : YmnosStructureRuleSerializer, IRuleSerializer<ApostichaRule>
    {
        public ApostichaRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.ApostichaNode,
                RuleConstants.LitiNode };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new ApostichaRule(new ApostichaVMFactory(SerializerRoot), req.Descriptor.GetElementName());
        }
    }
}
