using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class OrSerializer : LogicalExpressionSerializer, IRuleSerializer<Or>
    {
        public OrSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.OrNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new Or(req.Descriptor.GetElementName());
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
