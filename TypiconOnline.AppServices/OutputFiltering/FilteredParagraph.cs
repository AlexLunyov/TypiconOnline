using System;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Output;

namespace TypiconOnline.AppServices.OutputFiltering
{
    /// <summary>
    /// Выходная модель для параграфа с определением стиля
    /// </summary>
    [Serializable]
    //[XmlRoot(OutputConstants.ParagraphNodeName)]
    public class FilteredParagraph
    {
        [XmlElement(OutputConstants.ParagraphTextNodeName)]
        public ItemTextUnit Text { get; set; }
        [XmlElement(OutputConstants.ParagraphStyleNodeName)]
        public TextStyle Style { get; set; }
    }
}
