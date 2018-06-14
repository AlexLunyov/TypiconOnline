using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class IntSerializer : RuleXmlSerializerBase, IRuleSerializer<Int>
    {
        public IntSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.IntNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new Int(req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            (req.Element as Int).ValueExpression = new ItemInt(req.Descriptor.Element.InnerText);
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
