using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Extensions;
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

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new WorshipRule(req.Descriptor.GetElementName(), req.Parent);
        }

        protected override void FillObject(FillObjectRequest req)
        {
            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.WorshipRuleIdAttrName];
            (req.Element as WorshipRule).Id = (attr != null) ? attr.Value : string.Empty;

            attr = req.Descriptor.Element.Attributes[RuleConstants.WorshipRuleTimeAttrName];
            (req.Element as WorshipRule).Time = new ItemTime((attr != null) ? attr.Value : string.Empty);

            (req.Element as WorshipRule).Name = req.Descriptor.Element.GetItemTextStyled(RuleConstants.WorshipRuleNameNode);

            (req.Element as WorshipRule).AdditionalName = req.Descriptor.Element.GetItemTextStyled(RuleConstants.WorshipRuleAdditionalNameNode);

            attr = req.Descriptor.Element.Attributes[RuleConstants.WorshipRuleIsDayBeforeAttrName];
            if (bool.TryParse(attr?.Value, out bool showPsalm))
            {
                (req.Element as WorshipRule).IsDayBefore = showPsalm;
            }

            //sequence
            XmlNode sequenceNode = req.Descriptor.Element.SelectSingleNode(RuleConstants.WorshipRuleSequenceNode);
            if (sequenceNode != null)
            {
                (req.Element as WorshipRule).Sequence = SerializerRoot.Container<ExecContainer>()
                    .Deserialize(new XmlDescriptor() { Element = sequenceNode }, req.Parent);
            }

            (req.Element as IAsAdditionElement).FillElement(req.Descriptor.Element);
        }
    }
}
