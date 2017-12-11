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
    public class KSedalenRuleSerializer : KanonasItemRuleBaseSerializer, IRuleSerializer<KSedalenRule>
    {
        public KSedalenRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.KSedalenNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d) => new KSedalenRule(d.GetElementName());

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            base.FillObject(d, element);

            XmlAttribute attr = d.Element.Attributes[RuleConstants.KSedalenPlaceAttrName];
            if (Enum.TryParse(attr?.Value, true, out KanonasPlaceKind place))
            {
                (element as KSedalenRule).Place = place;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
