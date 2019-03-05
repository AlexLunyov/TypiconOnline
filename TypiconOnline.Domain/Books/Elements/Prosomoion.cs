using System;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Описание подобна
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = ElementConstants.ProsomoionNode)]
    public class Prosomoion : ItemText
    {
        public Prosomoion(Prosomoion source) : base(source)
        {
            Self = source.Self;
        }

        public Prosomoion() : base() { }

        /// <summary>
        /// Если true, то самоподобен
        /// </summary>
        [XmlAttribute(ElementConstants.ProsomoionSelfAttr)]
        public bool Self { get; set; }

        public bool Equals(Prosomoion item)
        {
            return base.Equals(item) && Self.Equals(item.Self);
        }
    }
}
