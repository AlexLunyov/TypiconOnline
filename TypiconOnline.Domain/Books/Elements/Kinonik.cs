using System;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Причастен
    /// </summary>
    [Serializable]
    public class Kinonik : ItemText
    {
        /// <summary>
        /// Разновидность песнопения (троичен, богородиен, мученичен...)
        /// </summary>
        [XmlAttribute(ElementConstants.OdiTroparionKindAttr)]
        public YmnosKind Kind { get; set; } = YmnosKind.Ymnos;
    }
}
