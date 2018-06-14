using System;
using System.Xml.Serialization;
using TypiconOnline.Domain.Books.Elements;

namespace TypiconOnline.Domain.ItemTypes
{
    /// <summary>
    /// Определение стиля для элементов правил
    /// </summary>
    [Serializable]
    public class TextStyle
    {
        [XmlElement(ElementConstants.StyleRedNodeName)]
        public bool IsRed { get; set; } = false;
        [XmlElement(ElementConstants.StyleBoldNodeName)]
        public bool IsBold { get; set; } = false;
        [XmlElement(ElementConstants.StyleItalicNodeName)]
        public bool IsItalic { get; set; } = false;
        [XmlElement(ElementConstants.StyleHeaderNodeName)]
        public HeaderCaption Header { get; set; } = HeaderCaption.NotDefined;
    }

    [Serializable]
    public enum HeaderCaption
    {
        [XmlEnum(Name = "notdefined")]
        NotDefined = 0,
        [XmlEnum(Name = "h1")]
        h1 = 1,
        [XmlEnum(Name = "h2")]
        h2 = 2,
        [XmlEnum(Name = "h3")]
        h3 = 3,
        [XmlEnum(Name = "h4")]
        h4 = 4
    }
}
