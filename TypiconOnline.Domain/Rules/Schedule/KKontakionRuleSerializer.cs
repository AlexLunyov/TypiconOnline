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
    public class KKontakionRuleSerializer : RuleXmlSerializerBase, IRuleSerializer<KKontakionRule>
    {
        public KKontakionRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KKontakionNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d) => new KKontakionRule(d.GetElementName());

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            XmlAttribute attr = d.Element.Attributes[RuleConstants.KKontakionSourceAttrName];
            if (Enum.TryParse(attr?.Value, true, out KanonasSource source))
            {
                (element as KKontakionRule).Source = source;
            }

            attr = d.Element.Attributes[RuleConstants.KKontakionKanonasAttrName];
            if (Enum.TryParse(attr?.Value, true, out KanonasKind kanonas))
            {
                (element as KKontakionRule).Kanonas = kanonas;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
