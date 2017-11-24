using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class WorshipRuleSerializer : YmnosStructureRuleSerializer, IRuleSerializer<WorshipRule>
    {
        public WorshipRuleSerializer(IRuleSerializerRoot root) : base(root)
        {
            ElementNames = new string[] {
                RuleConstants.ServiceNodeName };
        }

        protected override RuleElement CreateObject(XmlDescriptor d)
        {
            return new WorshipRule(d.GetElementName());
        }

        protected override void FillObject(XmlDescriptor d, RuleElement container)
        {
            base.FillObject(d, container);

            XmlAttribute attr = d.Element.Attributes[RuleConstants.ServiceTimeAttrName];
            (container as WorshipRule).Time = new ItemTime((attr != null) ? attr.Value : string.Empty);

            attr = d.Element.Attributes[RuleConstants.ServiceNameAttrName];
            (container as WorshipRule).Name = (attr != null) ? attr.Value : string.Empty;

            attr = d.Element.Attributes[RuleConstants.ServiceIsDayBeforeAttrName];
            if (bool.TryParse(attr?.Value, out bool showPsalm))
            {
                (container as WorshipRule).IsDayBefore = showPsalm;
            }

            attr = d.Element.Attributes[RuleConstants.ServiceAdditionalNameAttrName];
            (container as WorshipRule).AdditionalName = (attr != null) ? attr.Value : string.Empty;
        }
    }
}
