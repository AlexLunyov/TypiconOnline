using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.ViewModels
{
    [Serializable]
    [XmlRoot(ViewModelConstants.WorshipRuleNodeName)]
    public class WorshipRuleViewModel
    {
        public WorshipRuleViewModel() { }

        public WorshipRuleViewModel(WorshipRule worship)
        {
            Id = worship.Id;
            Name = worship.Name;
            Time = worship.Time.Expression;
            IsDayBefore = worship.IsDayBefore;
            AdditionalName = worship.AdditionalName;
        }

        [XmlAttribute(ViewModelConstants.WorshipRuleIdAttrName)]
        public string Id { get; set; }
        [XmlAttribute(ViewModelConstants.WorshipRuleTimeAttrName)]
        public string Time { get; set; }
        [XmlAttribute(ViewModelConstants.WorshipRuleNameAttrName)]
        public string Name { get; set; }
        [XmlIgnore]
        public bool IsDayBefore { get; set; }
        [XmlAttribute(ViewModelConstants.WorshipRuleAdditionalNameAttrName)]
        public string AdditionalName { get; set; }
        [XmlArray(ViewModelConstants.WorshipRuleChildNodeName)]
        [XmlArrayItem(ElementName = ViewModelConstants.ViewModelItemNodeName, Type = typeof(ViewModelItem))]
        public List<ViewModelItem> ChildElements { get; set; } = new List<ViewModelItem>();
    }
}
