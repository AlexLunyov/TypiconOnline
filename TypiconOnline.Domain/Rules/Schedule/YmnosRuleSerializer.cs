using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class YmnosRuleSerializer : RuleXmlSerializerBase, IRuleSerializer<YmnosRule>
    {
        public YmnosRuleSerializer(IRuleSerializerRoot unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] {
                RuleConstants.YmnosRuleNode,
                RuleConstants.YmnosRuleDoxastichonNode };
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            throw new NotImplementedException();
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
