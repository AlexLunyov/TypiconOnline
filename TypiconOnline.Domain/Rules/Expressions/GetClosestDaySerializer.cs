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
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class GetClosestDaySerializer : RuleXmlSerializerBase, IRuleSerializer<GetClosestDay>
    {
        public GetClosestDaySerializer(IRuleSerializerRoot unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] { RuleConstants.GetClosestDayNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new GetClosestDay(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            XmlAttribute attr = d.Element.Attributes[RuleConstants.DayOfWeekAttrName];
            if (attr != null)
            {
                (element as GetClosestDay).DayOfWeek = new ItemDayOfWeek(attr.Value);
            }

            attr = d.Element.Attributes[RuleConstants.WeekCountAttrName];

            if (int.TryParse(attr?.Value, out int count))
            {
                (element as GetClosestDay).WeekCount = count;
            }

            if (d.Element.HasChildNodes)
            {
                (element as GetClosestDay).ChildDateExp = _unitOfWork.Factory<DateExpression>()
                    .CreateElement(new XmlDescriptor() { Element = d.Element.FirstChild });
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
