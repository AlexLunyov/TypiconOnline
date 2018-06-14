using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class SwitchSerializer : RuleXmlSerializerBase, IRuleSerializer<Switch>
    {
        public SwitchSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.SwitchNodeName };
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new Switch(req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            //ищем expression
            XmlNode expressionNode = req.Descriptor.Element.SelectSingleNode(RuleConstants.ExpressionNodeName);

            if (expressionNode?.HasChildNodes == true)
            {
                (req.Element as Switch).Expression = SerializerRoot.Container<RuleExpression>()
                    .Deserialize(new XmlDescriptor() { Element = expressionNode.FirstChild }, req.Parent);
            }

            //ищем элементы case
            XmlNodeList casesList = req.Descriptor.Element.SelectNodes(RuleConstants.CaseNodeName);

            if (casesList != null)
            {
                foreach (XmlNode caseNode in casesList)
                {
                    Case caseElement = SerializerRoot.Container<Case>().Deserialize(new XmlDescriptor() { Element = caseNode }, req.Parent);
                    (req.Element as Switch).CaseElements.Add(caseElement);
                }
            }

            //ищем default
            XmlNode defaultNode = req.Descriptor.Element.SelectSingleNode(RuleConstants.DefaultNodeName);
            if (defaultNode != null)
            {
                (req.Element as Switch).Default = SerializerRoot.Container<ExecContainer>()
                    .Deserialize(new XmlDescriptor() { Element = defaultNode }, req.Parent);
            }
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
