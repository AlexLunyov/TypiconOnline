using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Models
{    
    [Serializable]
    public class WorshipModel
    {
        [XmlAttribute(RuleConstants.WorshipRuleTimeAttrName)]
        public string Time { get; set; }
        [XmlAttribute(RuleConstants.WorshipRuleModeAttrName)]
        public WorshipMode Mode { get; set; } = WorshipMode.ThisDay;
        [XmlElement(RuleConstants.WorshipRuleNameNode)]
        public ItemTextStyled Name { get; set; }
        [XmlElement(RuleConstants.WorshipRuleAdditionalNameNode)]
        public ItemTextStyled AdditionalName { get; set; }
    }
}
