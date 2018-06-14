using System;
using System.Xml;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class TheotokionAppRuleSerializer : YmnosRuleSerializer, IRuleSerializer<TheotokionAppRule>
    {
        public TheotokionAppRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.TheotokionAppRuleNode };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new TheotokionAppRule(req.Descriptor.GetElementName(), SerializerRoot.QueryProcessor);
        }

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            var obj = req.Element as TheotokionAppRule;

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.TheotokionAppRulePlaceAttrName];
            if (Enum.TryParse(attr?.Value, true, out TheotokionAppPlace place))
            {
                obj.Place = place;
            }

            if (req.Descriptor.Element.SelectSingleNode(RuleConstants.YmnosRuleNode) is XmlNode ymnosNode)
            {
                obj.ReferenceYmnos = SerializerRoot.Container<YmnosRule>()
                    .Deserialize(new XmlDescriptor() { Element = ymnosNode }, req.Parent);
            }
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
