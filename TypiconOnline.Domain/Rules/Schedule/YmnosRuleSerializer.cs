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
    public class YmnosRuleSerializer : RuleXmlSerializerBase, IRuleSerializer<YmnosRule>
    {
        public YmnosRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.YmnosRuleNode,
                RuleConstants.YmnosRuleDoxastichonNode };
        }

        protected override RuleElement CreateObject(XmlDescriptor d) => new YmnosRule(d.GetElementName());

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            if (Enum.TryParse(d.Element.Name, true, out YmnosRuleKind kind))
            {
                (element as YmnosRule).Kind = kind;
            }

            XmlAttribute attr = d.Element.Attributes[RuleConstants.YmnosRuleSourceAttrName];
            if (Enum.TryParse(attr?.Value, true, out YmnosSource source))
            {
                (element as YmnosRule).Source = source;
            }

            attr = d.Element.Attributes[RuleConstants.YmnosRulePlaceAttrName];
            if (Enum.TryParse(attr?.Value, true, out PlaceYmnosSource place))
            {
                (element as YmnosRule).Place = place;
            }

            attr = d.Element.Attributes[RuleConstants.YmnosRuleCountAttrName];
            (element as YmnosRule).Count = int.TryParse(attr?.Value, out int intValue) ? intValue : 0;

            attr = d.Element.Attributes[RuleConstants.YmnosRuleStartFromAttrName];
            (element as YmnosRule).StartFrom = int.TryParse(attr?.Value, out intValue) ? intValue : 0;
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
