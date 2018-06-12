using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class IsTwoSaintsSerializer : RuleXmlSerializerBase, IRuleSerializer<IsTwoSaints>
    {
        public IsTwoSaintsSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.IsTwoSaintsNodeName };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new IsTwoSaints(req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
