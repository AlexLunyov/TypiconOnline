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
    public class DateSerializer : RuleXmlSerializerBase, IRuleSerializer<Date>
    {
        public DateSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.DateNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new Date(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            (element as Date).ValueExpression = new ItemDate(d.Element.InnerText);
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
