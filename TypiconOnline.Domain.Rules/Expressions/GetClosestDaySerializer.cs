using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class GetClosestDaySerializer : RuleXmlSerializerBase, IRuleSerializer<GetClosestDay>
    {
        public GetClosestDaySerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.GetClosestDayNodeName };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new GetClosestDay(req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.DayOfWeekAttrName];
            if (attr != null)
            {
                (req.Element as GetClosestDay).DayOfWeek = new ItemDayOfWeek(attr.Value);
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

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
