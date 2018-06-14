using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class MoreEqualsSerializer : LogicalExpressionSerializer, IRuleSerializer<MoreEquals>
    {
        public MoreEqualsSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.MoreEqualsNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new MoreEquals(req.Descriptor.GetElementName());
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
