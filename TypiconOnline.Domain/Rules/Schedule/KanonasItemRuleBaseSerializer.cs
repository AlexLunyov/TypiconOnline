using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public abstract class KanonasItemRuleBaseSerializer : RuleXmlSerializerBase//, IRuleSerializer<KanonasItemRuleBase>
    {
        public KanonasItemRuleBaseSerializer(IRuleSerializerRoot root) : base(root) { }

        //protected override RuleElement CreateObject(XmlDescriptor d) => new KontakionRule(d.GetElementName());

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            XmlAttribute attr = d.Element.Attributes[RuleConstants.KKontakionSourceAttrName];
            if (Enum.TryParse(attr?.Value, true, out KanonasSource source))
            {
                (element as KanonasItemRuleBase).Source = source;
            }

            attr = d.Element.Attributes[RuleConstants.KKontakionKanonasAttrName];
            if (Enum.TryParse(attr?.Value, true, out KanonasKind kanonas))
            {
                (element as KanonasItemRuleBase).Kanonas = kanonas;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
