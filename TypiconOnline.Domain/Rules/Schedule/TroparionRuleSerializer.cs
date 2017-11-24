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
    public class TroparionRuleSerializer : YmnosStructureRuleSerializer
    {
        public TroparionRuleSerializer(IRuleSerializerUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] {
                RuleConstants.TroparionNode };
        }

        protected override ExecContainer CreateObject(XmlDescriptor d)
        {
            return new TroparionRule(d.GetElementName());
        }
    }
}
