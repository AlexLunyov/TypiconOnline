using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Output
{
    /// <summary>
    /// Элементарная частичка выходной локализованной формы последовательности богослужений
    /// </summary>
    [Serializable]
    [XmlRoot(OutputConstants.LocalizedOutputSectionNodeName)]
    public class LocalizedOutputSection
    {
        [XmlElement(OutputConstants.LocalizedOutputSectionKindTextNodeName)]
        public ItemTextUnit KindText { get; set; }

        [XmlAttribute(OutputConstants.LocalizedOutputSectionKindAttrName)]
        public ElementViewModelKind Kind { get; set; }

        [XmlArray(ElementName = OutputConstants.LocalizedOutputSectionChildNodeName, IsNullable = false)]
        [XmlArrayItem(ElementName = OutputConstants.ParagraphNodeName, Type = typeof(LocalizedParagraphNoted))]
        public List<LocalizedParagraphNoted> Paragraphs { get; set; }
    }
}
