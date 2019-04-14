using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Rules.Output.Factories;
using TypiconOnline.Domain.ItemTypes;
using System.Xml;
using TypiconOnline.Domain.Rules.Extensions;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class SedalenRuleSerializer : RuleXmlSerializerBase, IRuleSerializer<SedalenRule>
    {
        public SedalenRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.SedalenRuleNode };
        }

        public override string Serialize(IRuleElement element)
        {
            throw new System.NotImplementedException();
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new SedalenRule(new SedalenVMFactory(SerializerRoot), req.Descriptor.GetElementName());
        }

        protected override void FillObject(FillObjectRequest req)
        {
            var sedalenRule = req.Element as SedalenRule;

            var node = req.Descriptor.Element.SelectSingleNode(RuleConstants.SedalenRuleHeaderNode);
            if (node != null)
            {
                sedalenRule.Header = SerializerRoot.TypiconSerializer.Deserialize<ItemTextHeader>(node.OuterXml, RuleConstants.SedalenRuleHeaderNode);
            }

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.TotalCountAttribute];
            if (int.TryParse(attr?.Value, out int count))
            {
                sedalenRule.TotalYmnosCount = count;
            }

            node = req.Descriptor.Element.SelectSingleNode(RuleConstants.SedalenRuleDefinitionNode);
            if (node?.HasChildNodes != null)
            {
                var children = node.ChildNodes.DeserializeChildren(SerializerRoot, req.Parent);

                sedalenRule.ChildElements.AddRange(children);
            }
        }
    }
}
