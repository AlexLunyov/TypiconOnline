using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class DateSerializer : RuleXmlSerializerBase, IRuleSerializer<Date>
    {
        public DateSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.DateNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new Date(req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            (req.Element as Date).ValueExpression = new ItemDate(req.Descriptor.Element.InnerText);
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
