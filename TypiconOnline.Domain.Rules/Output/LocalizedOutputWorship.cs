using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Output
{
    [Serializable]
    [XmlRoot(OutputConstants.LocalizedOutputWorshipNodeName)]
    public class LocalizedOutputWorship
    {
        [XmlAttribute(OutputConstants.LocalizedOutputWorshipIdAttrName)]
        public string Id { get; set; }
        [XmlAttribute(OutputConstants.LocalizedOutputWorshipTimeAttrName)]
        public string Time { get; set; }
        [XmlElement(OutputConstants.LocalizedOutputWorshipNameNode)]
        public LocalizedParagraph Name { get; set; }
        [XmlElement(OutputConstants.LocalizedOutputWorshipAdditionalNameAttrName)]
        public ItemTextUnit AdditionalName { get; set; }
        [XmlArray(OutputConstants.LocalizedOutputWorshipChildNodeName)]
        [XmlArrayItem(ElementName = OutputConstants.LocalizedOutputSectionNodeName, Type = typeof(LocalizedOutputSection))]
        public List<LocalizedOutputSection> ChildElements { get; set; } = new List<LocalizedOutputSection>();
    }
}
