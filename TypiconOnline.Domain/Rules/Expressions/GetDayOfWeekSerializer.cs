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
    public class GetDayOfWeekSerializer : RuleXmlSerializerBase, IRuleSerializer<GetDayOfWeek>
    {
        public GetDayOfWeekSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.GetDayOfWeekNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new GetDayOfWeek(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            XmlAttribute attr = d.Element.Attributes[RuleConstants.GetDayOfWeekAttrName];
            if (attr != null)
            {
                (element as GetDayOfWeek).DayOfWeek = new ItemDayOfWeek(attr.Value);
            }

            if (d.Element.HasChildNodes)
            {
                (element as GetDayOfWeek).ChildDateExp = SerializerRoot.Container<DateExpression>()
                    .Deserialize(new XmlDescriptor() { Element = d.Element.FirstChild });
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
