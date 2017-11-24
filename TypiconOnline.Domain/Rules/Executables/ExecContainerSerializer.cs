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
        public ExecContainerSerializer(IRuleSerializerUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] { RuleConstants.ExecContainerNodeName,
                                          RuleConstants.ActionNodeName };
        }

        public virtual RuleElement Deserialize(IDescriptor descriptor)
        {
            ExecContainer element = null;

            if (descriptor is XmlDescriptor d)
            {
                element = CreateObject(d);

                FillObject(d, element);
            }

            return element;
        }

        public string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }

        protected virtual ExecContainer CreateObject(XmlDescriptor d)
        {
            return new ExecContainer(d.GetElementName());
        }

        protected virtual void FillObject(XmlDescriptor d, ExecContainer container)
        {
            foreach (XmlNode childNode in d.Element.ChildNodes)
            {
                RuleElement child = _unitOfWork.Factory<RuleElement>().CreateElement(new XmlDescriptor() { Element = childNode });
                container.ChildElements.Add(child);
            }
        }
    }
}
