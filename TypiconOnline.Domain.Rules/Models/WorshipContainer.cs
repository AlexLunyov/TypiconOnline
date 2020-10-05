using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Rules.Models
{
    [Serializable]
    [XmlRoot(RuleConstants.ExecContainerNodeName)]
    public class WorshipContainer
    {
        [XmlElement(ElementName = RuleConstants.WorshipRuleNodeName, Type = typeof(WorshipModel))]
        public List<WorshipModel> Worships { get; set; } = new List<WorshipModel>();
    }
}
