using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KSedalenTheotokionRuleSerializer : KSedalenRuleSerializer , IRuleSerializer<KSedalenTheotokionRule>
    {
        public KSedalenTheotokionRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KSedalenTheotokionNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d) => new KSedalenTheotokionRule(d.GetElementName());

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
