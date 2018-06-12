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

        //protected override RuleElement CreateObject(CreateObjectRequest req) => new KontakionRule(req.Descriptor.GetElementName());

        protected override void FillObject(FillObjectRequest req)
        {
            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.KanonasSourceAttrName];
            if (Enum.TryParse(attr?.Value, true, out KanonasSource source))
            {
                (req.Element as KanonasItemRuleBase).Source = source;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.KanonasKindAttrName];
            if (Enum.TryParse(attr?.Value, true, out KanonasKind kanonas))
            {
                (req.Element as KanonasItemRuleBase).Kanonas = kanonas;
            }
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
