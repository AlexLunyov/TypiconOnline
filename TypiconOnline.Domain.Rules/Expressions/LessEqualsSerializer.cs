using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class LessEqualsSerializer : LogicalExpressionSerializer, IRuleSerializer<LessEquals>
    {
        public LessEqualsSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.LessEqualsNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new LessEquals(req.Descriptor.GetElementName());
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
