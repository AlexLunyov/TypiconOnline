using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class ExecContainerSerializer : RuleXmlSerializerBase, IRuleSerializer<ExecContainer>
    {
        public ExecContainerSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.ExecContainerNodeName,
                                          RuleConstants.ActionNodeName };
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new ExecContainer(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            foreach (XmlNode childNode in d.Element.ChildNodes)
            {
                RuleElement child = SerializerRoot.Factory<RuleElement>().CreateElement(new XmlDescriptor() { Element = childNode });
                (element as ExecContainer).ChildElements.Add(child);
            }
        }
    }
}
