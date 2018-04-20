using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class IfSerializer : RuleXmlSerializerBase, IRuleSerializer<If>
    {
        public IfSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.IfNodeName };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req) => new If(req.Descriptor.GetElementName());

        protected override void FillObject(FillObjectRequest req)
        {
            //expression
            XmlNode elementNode = req.Descriptor.Element.SelectSingleNode(RuleConstants.ExpressionIfNodeName);
            if (elementNode?.HasChildNodes == true)
            {
                (req.Element as If).Expression = SerializerRoot.Container<BooleanExpression>()
                    .Deserialize(new XmlDescriptor() { Element = elementNode.FirstChild }, req.Parent);
            }

            //ищем then
            elementNode = req.Descriptor.Element.SelectSingleNode(RuleConstants.ThenNodeName);
            if (elementNode != null)
            {
                (req.Element as If).ThenElement = SerializerRoot.Container<ExecContainer>()
                    .Deserialize(new XmlDescriptor() { Element = elementNode }, req.Parent);
            }

            //ищем else
            elementNode = req.Descriptor.Element.SelectSingleNode(RuleConstants.ElseNodeName);
            if (elementNode != null)
            {
                (req.Element as If).ElseElement = SerializerRoot.Container<ExecContainer>()
                    .Deserialize(new XmlDescriptor() { Element = elementNode }, req.Parent);
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
