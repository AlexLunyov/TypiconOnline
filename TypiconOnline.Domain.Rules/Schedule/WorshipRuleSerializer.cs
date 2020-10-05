using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Variables;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class WorshipRuleSerializer : RuleXmlSerializerBase, IRuleSerializer<WorshipRule>
    {
        public WorshipRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.WorshipRuleNodeName };
        }

        public override string Serialize(IRuleElement element)
        {
            throw new NotImplementedException();
        }

        protected override IRuleElement CreateObject(CreateObjectRequest req)
        {
            return new WorshipRule(req.Descriptor.GetElementName(), req.Parent, SerializerRoot.QueryProcessor);
        }

        protected override void FillObject(FillObjectRequest req)
        {
            var obj = req.Element as WorshipRule;

            var attr = req.Descriptor.Element.Attributes[RuleConstants.WorshipRuleIdAttrName];
            obj.Id = (attr != null) ? attr.Value : string.Empty;

            attr = req.Descriptor.Element.Attributes[RuleConstants.WorshipRuleTimeAttrName];
            obj.Time = new VariableItemTime((attr != null) ? attr.Value : string.Empty);

            obj.Name = req.Descriptor.Element
                .GetItemTextStyled(SerializerRoot.TypiconSerializer, RuleConstants.WorshipRuleNameNode);

            obj.AdditionalName = req.Descriptor.Element
                .GetItemTextStyled(SerializerRoot.TypiconSerializer, RuleConstants.WorshipRuleAdditionalNameNode);

            attr = req.Descriptor.Element.Attributes[RuleConstants.WorshipRuleModeAttrName];
            if (Enum.TryParse(attr?.Value, true, out WorshipMode source))
            {
                obj.Mode = source;
            }

            //sequence
            var sequenceNode = req.Descriptor.Element.SelectSingleNode(RuleConstants.WorshipRuleSequenceNode);
            if (sequenceNode != null)
            {
                obj.Sequence = SerializerRoot.Container<ExecContainer>()
                    .Deserialize(new XmlDescriptor() { Element = sequenceNode }, req.Parent);
            }

            obj.FillIAsAdditionElement(req.Descriptor.Element);
        }
    }
}
