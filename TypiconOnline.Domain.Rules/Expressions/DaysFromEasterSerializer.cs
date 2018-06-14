using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class DaysFromEasterSerializer : RuleXmlSerializerBase, IRuleSerializer<DaysFromEaster>
    {
        public DaysFromEasterSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.DaysFromEasterNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new DaysFromEaster(req.Descriptor.GetElementName(), SerializerRoot.QueryProcessor);
        }

        protected override void FillObject(FillObjectRequest req)
        {
            if (req.Descriptor.Element.HasChildNodes)
            {
                (req.Element as DaysFromEaster).ChildExpression = SerializerRoot.Container<DateExpression>()
                    .Deserialize(new XmlDescriptor() { Element = req.Descriptor.Element.FirstChild }, req.Parent);
            }
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
