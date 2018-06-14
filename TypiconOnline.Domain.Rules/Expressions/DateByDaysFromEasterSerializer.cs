using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class DateByDaysFromEasterSerializer : RuleXmlSerializerBase, IRuleSerializer<DateByDaysFromEaster>
    {
        public DateByDaysFromEasterSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.DateByDaysFromEasterNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new DateByDaysFromEaster(req.Descriptor.GetElementName(), SerializerRoot.QueryProcessor);
        }

        protected override void FillObject(FillObjectRequest req)
        {
            if (req.Descriptor.Element.HasChildNodes)
            {
                (req.Element as DateByDaysFromEaster).ChildExpression = SerializerRoot.Container<IntExpression>()
                    .Deserialize(new XmlDescriptor() { Element = req.Descriptor.Element.FirstChild }, req.Parent);
            }
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
