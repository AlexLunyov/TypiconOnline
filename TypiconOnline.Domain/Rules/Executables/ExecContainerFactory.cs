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
    public class ExecContainerFactory : RuleXmlFactoryBase, IRuleFactory<ExecContainer>
    {
        /// <summary>
        /// Обязательное для заполнения!!
        /// </summary>
        public ExecContainerFactory(IRuleFactoryUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] { RuleConstants.ExecContainerNodeName,
                                          RuleConstants.ActionNodeName };
        }

        public ExecContainer Create(IDescriptor descriptor)
        {
            ExecContainer element = null;

            if (descriptor is XmlDescriptor d)
            {
                element = new ExecContainer(d.GetElementName());

                foreach (XmlNode childNode in d.Element.ChildNodes)
                {
                    RuleElement child = _unitOfWork.Factory<RuleElement>().CreateElement(new XmlDescriptor() { Element = childNode });
                    element.ChildElements.Add(child);
                }
            }

            return element;
        }
    }
}
