using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.ViewModels
{
    [Serializable]
    [XmlRoot(ViewModelConstants.WorshipRuleNodeName)]
    public class WorshipRuleViewModel
    {
        public WorshipRuleViewModel() { }

        public WorshipRuleViewModel(WorshipRule worship, string language)
        {
            Id = worship.Id;
            Time = worship.Time.Expression;
            IsDayBefore = worship.IsDayBefore;
            Name = worship.Name?.FirstOrDefault(language);
            AdditionalName = worship.AdditionalName?.FirstOrDefault(language);
        }

        [XmlAttribute(ViewModelConstants.WorshipRuleIdAttrName)]
        public string Id { get; set; }
        [XmlAttribute(ViewModelConstants.WorshipRuleTimeAttrName)]
        public string Time { get; set; }
        [XmlElement(ViewModelConstants.WorshipRuleNameAttrName)]
        public ItemTextUnit Name { get; set; }
        [XmlIgnore]
        public bool IsDayBefore { get; set; }
        [XmlElement(ViewModelConstants.WorshipRuleAdditionalNameAttrName)]
        public ItemTextUnit AdditionalName { get; set; }
        [XmlArray(ViewModelConstants.WorshipRuleChildNodeName)]
        [XmlArrayItem(ElementName = ViewModelConstants.ElementViewModelNodeName, Type = typeof(ElementViewModel))]
        public List<ElementViewModel> ChildElements { get; set; } = new List<ElementViewModel>();
    }
}
