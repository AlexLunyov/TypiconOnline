using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class CaseSerializer : RuleXmlSerializerBase, IRuleSerializer<Case>
    {
        public CaseSerializer(IRuleSerializerRoot unitOfWork) : base(unitOfWork)
        {
            ElementNames = new string[] { RuleConstants.CaseNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new Case(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            XmlNode valuesNode = d.Element.SelectSingleNode(RuleConstants.ValuesNodeName);

            if (valuesNode?.HasChildNodes == true)
            {
                foreach (XmlNode valueNode in valuesNode.ChildNodes)
                {
                    RuleExpression valueElement = _unitOfWork.Factory<RuleExpression>()
                    .CreateElement(new XmlDescriptor() { Element = valueNode });

                    (element as Case).ValuesElements.Add(valueElement);
                }
            }

            XmlNode actionNode = d.Element.SelectSingleNode(RuleConstants.ActionNodeName);

            if (actionNode != null)
            {
                (element as Case).ActionElement = _unitOfWork.Factory<ExecContainer>()
                    .CreateElement(new XmlDescriptor() { Element = actionNode });
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
