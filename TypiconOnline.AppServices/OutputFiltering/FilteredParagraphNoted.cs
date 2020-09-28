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
    public class FilteredParagraphNoted : FilteredParagraph
    {
        [XmlElement(OutputConstants.ParagraphNoteNodeName)]
        public FilteredParagraph Note { get; set; }
    }
}
