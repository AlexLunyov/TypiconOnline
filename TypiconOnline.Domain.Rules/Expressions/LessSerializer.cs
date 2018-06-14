using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class LessSerializer : LogicalExpressionSerializer, IRuleSerializer<Less>
    {
        public LessSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.LessNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new Less(req.Descriptor.GetElementName());
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
