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
            if (int.TryParse(attr?.Value, out int intValue))
            {
                (element as YmnosRule).Count = intValue;
            }

            attr = d.Element.Attributes[RuleConstants.YmnosRuleStartFromAttrName];
            if (int.TryParse(attr?.Value, out intValue))
            {
                (element as YmnosRule).StartFrom = intValue;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
