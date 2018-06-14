using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class MoreSerializer : LogicalExpressionSerializer, IRuleSerializer<More>
    {
        public MoreSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.MoreNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new More(req.Descriptor.GetElementName());
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
