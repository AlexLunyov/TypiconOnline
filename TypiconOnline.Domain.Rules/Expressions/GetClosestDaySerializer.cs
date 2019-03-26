using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class GetClosestDaySerializer : RuleXmlSerializerBase, IRuleSerializer<GetClosestDay>
    {
        public GetClosestDaySerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.GetClosestDayNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new GetClosestDay(req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.DayOfWeekAttrName];
            if (attr != null)
            {
                if (DayOfWeekParser.TryParse(attr.Value, out DayOfWeek? dayOfWeek))
                {
                    (req.Element as GetClosestDay).DayOfWeek = dayOfWeek.Value;
                }
                else
                {
                    //error
                }
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.WeekCountAttrName];

            if (int.TryParse(attr?.Value, out int count))
            {
                (req.Element as GetClosestDay).WeekCount = count;
            }

            if (req.Descriptor.Element.HasChildNodes)
            {
                (req.Element as GetClosestDay).ChildDateExp = SerializerRoot.Container<DateExpression>()
                    .Deserialize(new XmlDescriptor() { Element = req.Descriptor.Element.FirstChild }, req.Parent);
            }
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
