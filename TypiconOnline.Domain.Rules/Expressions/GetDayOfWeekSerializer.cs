using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class GetDayOfWeekSerializer : RuleXmlSerializerBase, IRuleSerializer<GetDayOfWeek>
    {
        public GetDayOfWeekSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.GetDayOfWeekNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new GetDayOfWeek(req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.GetDayOfWeekAttrName];
            if (attr != null)
            {
                (req.Element as GetDayOfWeek).DayOfWeek = new ItemDayOfWeek(attr.Value);
            }

            if (req.Descriptor.Element.HasChildNodes)
            {
                (req.Element as GetDayOfWeek).ChildDateExp = SerializerRoot.Container<DateExpression>()
                    .Deserialize(new XmlDescriptor() { Element = req.Descriptor.Element.FirstChild }, req.Parent);
            }
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
