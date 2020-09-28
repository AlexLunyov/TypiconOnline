using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Output;

namespace TypiconOnline.AppServices.OutputFiltering
{
    /// <summary>
    /// Элементарная частичка выходной локализованной формы последовательности богослужений
    /// </summary>
    [Serializable]
    [XmlRoot(OutputConstants.LocalizedOutputSectionNodeName)]
    public class FilteredOutputSection
    {
        [XmlElement(OutputConstants.LocalizedOutputSectionKindTextNodeName)]
        public ItemTextUnit KindText { get; set; }

        [XmlAttribute(OutputConstants.LocalizedOutputSectionKindAttrName)]
        public ElementViewModelKind Kind { get; set; }

        [XmlArray(ElementName = OutputConstants.LocalizedOutputSectionChildNodeName, IsNullable = false)]
        [XmlArrayItem(ElementName = OutputConstants.ParagraphNodeName, Type = typeof(FilteredParagraphNoted))]
        public List<FilteredParagraphNoted> Paragraphs { get; set; }
    }
}
