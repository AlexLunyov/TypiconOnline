using System;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class IsExistsSerializer : RuleXmlSerializerBase, IRuleSerializer<IsExists>
    {
        public IsExistsSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.IsExistsNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new IsExists(req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            if (req.Descriptor.Element.HasChildNodes)// && node.FirstChild.Name == RuleConstants.YmnosRuleNode)
            {
                (req.Element as IsExists).ChildElement = SerializerRoot.Container<RuleExecutable, ICalcStructureElement>()
                    .Deserialize(new XmlDescriptor() { Element = req.Descriptor.Element.FirstChild }, req.Parent) as ICalcStructureElement;
            }
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }

        
    }
}
