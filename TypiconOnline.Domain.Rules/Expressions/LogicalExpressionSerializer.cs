using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public abstract class LogicalExpressionSerializer : RuleXmlSerializerBase, IRuleSerializer<LogicalExpression>
    {
        public LogicalExpressionSerializer(IRuleSerializerRoot root) : base(root)
        {
        }

        protected override void FillObject(FillObjectRequest req)
        {
            foreach (XmlNode childNode in req.Descriptor.Element.ChildNodes)
            {
                var exp = SerializerRoot.Container<RuleExpression>()
                    .Deserialize(new XmlDescriptor() { Element = childNode }, req.Parent);

                (req.Element as LogicalExpression).ChildElements.Add(exp);
            }
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
