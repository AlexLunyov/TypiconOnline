using System;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Output
{
    /// <summary>
    /// Выходная модель для параграфа с определением стиля
    /// </summary>
    [Serializable]
    //[XmlRoot(OutputConstants.ParagraphNodeName)]
    public class LocalizedParagraph
    {
        [XmlElement(OutputConstants.ParagraphTextNodeName)]
        public ItemTextUnit Text { get; set; }
        [XmlElement(OutputConstants.ParagraphStyleNodeName)]
        public TextStyle Style { get; set; }
    }
}
