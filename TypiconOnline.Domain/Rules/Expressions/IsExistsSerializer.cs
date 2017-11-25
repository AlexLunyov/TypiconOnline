using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class IsExistsSerializer : RuleXmlSerializerBase, IRuleSerializer<IsExists>
    {
        public IsExistsSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.IsExistsNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new IsExists(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            if (d.Element.HasChildNodes)// && node.FirstChild.Name == RuleConstants.YmnosRuleNode)
            {
                (element as IsExists).ChildElement = SerializerRoot.Container<RuleExecutable, ICalcStructureElement>()
                    .Deserialize(new XmlDescriptor() { Element = d.Element.FirstChild }) as ICalcStructureElement;
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }

        
    }
}
