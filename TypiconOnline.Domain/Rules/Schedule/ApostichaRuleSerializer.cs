using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class ApostichaRuleSerializer : YmnosStructureRuleSerializer
    {
        public ApostichaRuleSerializer(IRuleSerializerRoot unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] {
                RuleConstants.ApostichaNode,
                RuleConstants.LitiNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new ApostichaRule(d.GetElementName());
        }
    }
}
