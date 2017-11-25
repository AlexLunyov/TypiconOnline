using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class SwitchSerializer : RuleXmlSerializerBase, IRuleSerializer<Switch>
    {
        public SwitchSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.SwitchNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new Switch(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            //ищем expression
            XmlNode expressionNode = d.Element.SelectSingleNode(RuleConstants.ExpressionNodeName);

            if (expressionNode?.HasChildNodes == true)
            {
                (element as Switch).Expression = SerializerRoot.Container<RuleExpression>()
                    .Deserialize(new XmlDescriptor() { Element = expressionNode.FirstChild });
            }

            //ищем элементы case
            XmlNodeList casesList = d.Element.SelectNodes(RuleConstants.CaseNodeName);

            if (casesList != null)
            {
                foreach (XmlNode caseNode in casesList)
                {
                    Case caseElement = SerializerRoot.Container<Case>().Deserialize(new XmlDescriptor() { Element = caseNode });
                    (element as Switch).CaseElements.Add(caseElement);
                }
            }

            //ищем default
            XmlNode defaultNode = d.Element.SelectSingleNode(RuleConstants.DefaultNodeName);
            if (defaultNode != null)
            {
                (element as Switch).Default = SerializerRoot.Container<ExecContainer>()
                    .Deserialize(new XmlDescriptor() { Element = defaultNode });
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
