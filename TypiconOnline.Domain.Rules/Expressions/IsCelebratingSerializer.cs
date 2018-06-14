using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class IsCelebratingSerializer : RuleXmlSerializerBase, IRuleSerializer<IsCelebrating>
    {
        public IsCelebratingSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.IsCelebratingNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new IsCelebrating(req.Descriptor.GetElementName());
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
