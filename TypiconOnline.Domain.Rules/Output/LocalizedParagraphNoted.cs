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
    public class LocalizedParagraphNoted : LocalizedParagraph
    {
        [XmlElement(OutputConstants.ParagraphNoteNodeName)]
        public LocalizedParagraph Note { get; set; }
    }
}
