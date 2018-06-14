using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Паремия
    /// </summary>
    [Serializable]
    public class Paroimia : DayElementBase
    {
        public Paroimia() { }

        /// <summary>
        /// Цитата Священного писания, например "1.15-17"
        /// </summary>
        [XmlAttribute(ElementConstants.ParoimiaQuoteAttr)]
        public string Quote { get; set; }

        /// <summary>
        /// Наименование книги
        /// Например, Притчей чтение
        /// </summary>
        [XmlElement(ElementConstants.ParoimiaBookNameNode)]
        public ItemText BookName { get; set; }

        private List<ItemText> _stihoi = new List<ItemText>();
        [XmlElement(ElementConstants.ParoimiaStihosNode)]
        public List<ItemText> Stihoi
        {
            get
            {
                return _stihoi;
            }
            set
            {
                _stihoi = value;
            }
        }

        protected override void Validate()
        {
            if (Stihoi == null || Stihoi.Count == 0)
            {
                AddBrokenConstraint(ProkeimenonBusinessConstraint.StihoiRequired);
            }
        }
    }
}