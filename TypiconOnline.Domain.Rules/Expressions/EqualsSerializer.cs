using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class EqualsSerializer : LogicalExpressionSerializer, IRuleSerializer<Equals>
    {
        public EqualsSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.EqualsNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new Equals(req.Descriptor.GetElementName());
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
