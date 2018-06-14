using System;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Элемент структуры эксапостилария
    /// </summary>
    [Serializable]
    public class ExapostilarionItem : DayElementBase
    {
        #region Properties
        /// <summary>
        /// Название подобна (самоподобна)
        /// </summary>
        [XmlElement(ElementConstants.ProsomoionNode)]
        public Prosomoion Prosomoion { get; set; }

        /// <summary>
        /// Дополнительные сведения. Например, "Феофаново" - Про стихиру
        /// </summary>
        [XmlElement(ElementConstants.AnnotationNode)]
        public ItemText Annotation { get; set; }

        /// <summary>
        /// Текст песнопения
        /// </summary>
        [XmlElement(ElementConstants.YmnosTextNode)]
        public ItemText Text { get; set; }
        #endregion

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
