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

        protected override RuleElement CreateObject(CreateObjectRequest req) => new KSedalenRule(req.Descriptor.GetElementName());

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.KSedalenPlaceAttrName];
            if (Enum.TryParse(attr?.Value, true, out KanonasPlaceKind place))
            {
                (req.Element as KSedalenRule).Place = place;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
