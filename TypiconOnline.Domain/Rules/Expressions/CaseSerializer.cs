using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Expressions
{
    public class CaseSerializer : RuleXmlSerializerBase, IRuleSerializer<Case>
    {
        public CaseSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] { RuleConstants.CaseNodeName };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new Case(req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            XmlNode valuesNode = req.Descriptor.Element.SelectSingleNode(RuleConstants.ValuesNodeName);

            if (valuesNode?.HasChildNodes == true)
            {
                foreach (XmlNode valueNode in valuesNode.ChildNodes)
                {
                    RuleExpression valueElement = SerializerRoot.Container<RuleExpression>()
                    .Deserialize(new XmlDescriptor() { Element = valueNode }, req.Parent);

                    (req.Element as Case).ValuesElements.Add(valueElement);
                }
            }

            XmlNode actionNode = req.Descriptor.Element.SelectSingleNode(RuleConstants.ActionNodeName);

            if (actionNode != null)
            {
                (req.Element as Case).ActionElement = SerializerRoot.Container<ExecContainer>()
                    .Deserialize(new XmlDescriptor() { Element = actionNode }, req.Parent);
            }
        }

        public override string Serialize(RuleElement element)
        {
            throw new NotImplementedException();
        }
    }
}
