using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class AndSerializer : LogicalExpressionSerializer, IRuleSerializer<And>
    {
        public AndSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.AndNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new And(req.Descriptor.GetElementName());
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
