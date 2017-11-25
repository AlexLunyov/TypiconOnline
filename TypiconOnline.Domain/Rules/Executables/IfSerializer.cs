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
    public class IfSerializer : RuleXmlSerializerBase, IRuleSerializer<If>
    {
        public IfSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.IfNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d) => new If(d.GetElementName());

        protected override void FillObject(XmlDescriptor d, RuleElement element)
        {
            //expression
            XmlNode elementNode = d.Element.SelectSingleNode(RuleConstants.ExpressionIfNodeName);
            if (elementNode?.HasChildNodes == true)
            {
                (element as If).Expression = SerializerRoot.Container<BooleanExpression>()
                    .Deserialize(new XmlDescriptor() { Element = elementNode.FirstChild });
            }

            //ищем then
            elementNode = d.Element.SelectSingleNode(RuleConstants.ThenNodeName);
            if (elementNode != null)
            {
                (element as If).ThenElement = SerializerRoot.Container<ExecContainer>()
                    .Deserialize(new XmlDescriptor() { Element = elementNode });
            }

            //ищем else
            elementNode = d.Element.SelectSingleNode(RuleConstants.ElseNodeName);
            if (elementNode != null)
            {
                (element as If).ElseElement = SerializerRoot.Container<ExecContainer>()
                    .Deserialize(new XmlDescriptor() { Element = elementNode });
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
