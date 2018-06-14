using System;
using System.Xml.Serialization;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Psalter
{
    [Serializable]
    [XmlRoot(ElementConstants.PsalmStihosNode)]
    public class PsalmStihos : ItemText, IPsalterElement
    {
        [XmlAttribute(ElementConstants.ReadingStihosNumberAttr)]
        public int Number { get; set; }
    }
}
