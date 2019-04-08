using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rules.Output
{
    [Serializable]
    [XmlRoot(OutputConstants.OutputWorshipNodeName)]
    public class OutputWorship : ILocalizable<LocalizedOutputWorship>
    {
        public OutputWorship() { }

        public OutputWorship(WorshipRule worship)
        {
            Id = worship.Id;
            Time = worship.Time.Expression;
            Name = worship.Name;
            AdditionalName = worship.AdditionalName;
        }

        [XmlAttribute(OutputConstants.OutputWorshipIdAttrName)]
        public string Id { get; set; }
        [XmlAttribute(OutputConstants.OutputWorshipTimeAttrName)]
        public string Time { get; set; }
        [XmlElement(OutputConstants.OutputWorshipNameAttrName)]
        public ItemTextStyled Name { get; set; }
        [XmlElement(OutputConstants.OutputWorshipAdditionalNameAttrName)]
        public ItemText AdditionalName { get; set; }
        [XmlArray(OutputConstants.OutputWorshipChildNodeName)]
        [XmlArrayItem(ElementName = OutputConstants.OutputSectionNodeName, Type = typeof(OutputSection))]
        public List<OutputSection> ChildElements { get; set; } = new List<OutputSection>();

        public LocalizedOutputWorship Localize(string language)
        {
            return new LocalizedOutputWorship
            {
                Id = Id,
                Time = Time,
                Name = Name.Localize(language),
                AdditionalName = AdditionalName?.Localize(language),
                ChildElements = ChildElements.Localize(language)
            };
        }
    }
}
