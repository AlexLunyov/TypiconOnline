using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class WorshipRuleSerializer : YmnosStructureRuleSerializer, IRuleSerializer<WorshipRule>
    {
        public WorshipRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.WorshipRuleNodeName };
        }

        protected override RuleElement CreateObject(CreateObjectRequest req)
        {
            return new WorshipRule(req.Descriptor.GetElementName(), req.Parent);
        }

        protected override void FillObject(FillObjectRequest req)
        {
            base.FillObject(req);

            XmlAttribute attr = req.Descriptor.Element.Attributes[RuleConstants.WorshipRuleIdAttrName];
            (req.Element as WorshipRule).Id = (attr != null) ? attr.Value : string.Empty;

            attr = req.Descriptor.Element.Attributes[RuleConstants.WorshipRuleTimeAttrName];
            (req.Element as WorshipRule).Time = new ItemTime((attr != null) ? attr.Value : string.Empty);

            attr = req.Descriptor.Element.Attributes[RuleConstants.WorshipRuleNameAttrName];
            (req.Element as WorshipRule).Name = (attr != null) ? attr.Value : string.Empty;

            attr = req.Descriptor.Element.Attributes[RuleConstants.WorshipRuleIsDayBeforeAttrName];
            if (bool.TryParse(attr?.Value, out bool showPsalm))
            {
                (req.Element as WorshipRule).IsDayBefore = showPsalm;
            }

            attr = req.Descriptor.Element.Attributes[RuleConstants.WorshipRuleAdditionalNameAttrName];
            (req.Element as WorshipRule).AdditionalName = (attr != null) ? attr.Value : string.Empty;

            (req.Element as IAsAdditionElement).FillElement(req.Descriptor.Element);
        }
    }
}
