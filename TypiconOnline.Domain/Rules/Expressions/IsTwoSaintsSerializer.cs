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

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new IsTwoSaints(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
