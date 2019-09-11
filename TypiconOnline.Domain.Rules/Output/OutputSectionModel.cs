using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Extensions;

namespace TypiconOnline.Domain.Rules.Output
{
    /// <summary>
    /// Раздел выходной формы последовательности богослужений
    /// </summary>
    [Serializable]
    [XmlRoot(OutputConstants.OutputSectionNodeName)]
    public class OutputSectionModel
    {
        [XmlElement(ElementName = OutputConstants.OutputSectionKindTextNodeName, IsNullable = true)]
        public ItemText KindText { get; set; }

        [XmlAttribute(OutputConstants.OutputSectionKindAttrName)]
        public ElementViewModelKind Kind { get; set; }

        [XmlArray(ElementName = OutputConstants.OutputSectionChildNodeName, IsNullable = true)]
        [XmlArrayItem(ElementName = OutputConstants.ParagraphNodeName, Type = typeof(ItemTextNoted))]
        public List<ItemTextNoted> Paragraphs { get; set; }
    }

    public enum ElementViewModelKind { Choir, Lector, Priest, Deacon, Stihos, Text, Irmos, Troparion, Chorus, Theotokion }
}
