using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class YmnosCustomRuleSerializer : RuleXmlSerializerBase, IRuleSerializer<YmnosCustomRule>
    {
        public YmnosCustomRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.YmnosCustomRuleNode };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req) => new YmnosCustomRule(req.Descriptor.GetElementName());

        protected override void FillObject(FillObjectRequest req)
        {
            YmnosCustomRule element = req.Element as YmnosCustomRule;

            if (req.Descriptor.Element.FirstChild is XmlNode node)
            {
                switch (node.Name)
                {
                    case RuleConstants.YmnosCustomRuleGroupNode:
                        {
                            element.Kind = YmnosRuleKind.Ymnos;
                            element.Element = Deserialize(node.OuterXml, RuleConstants.YmnosCustomRuleGroupNode);
                        }
                        break;
                    case RuleConstants.YmnosCustomRuleDoxastichonNode:
                        {
                            element.Kind = YmnosRuleKind.Doxastichon;
                            element.Element = Deserialize(node.OuterXml, RuleConstants.YmnosCustomRuleDoxastichonNode);
                        }
                        break;
                    case RuleConstants.YmnosCustomRuleTheotokionNode:
                        {
                            element.Kind = YmnosRuleKind.Theotokion;
                            element.Element = Deserialize(node.OuterXml, RuleConstants.YmnosCustomRuleTheotokionNode);
                        }
                        break;
                }
            }

            YmnosGroup Deserialize(string xml, string rootElement) => new TypiconSerializer().Deserialize<YmnosGroup>(xml, rootElement);
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
