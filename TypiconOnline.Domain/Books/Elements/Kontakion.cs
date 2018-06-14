using System;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Описание кондака, содержащегося в каноне утрени
    /// </summary>
    [Serializable]
    public class Kontakion : DayElementBase, IContainingIhos
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
        /// Глас
        /// </summary>
        [XmlAttribute(ElementConstants.YmnosIhosAttrName)]
        public int Ihos { get; set; }

        [XmlElement(ElementConstants.YmnosNode)]
        public ItemText Ymnos { get; set; }

        [XmlElement(ElementConstants.IkosNode)]
        public ItemText Ikos { get; set; }

        #endregion

        protected override void Validate()
        {
            if (Ymnos == null)
            {
                AddBrokenConstraint(KontakionBusinessConstraint.YmnosRequired);
            }

            //глас должен иметь значения с 1 до 8
            if ((Ihos < 1) || (Ihos > 8))
            {
                AddBrokenConstraint(YmnosGroupBusinessConstraint.InvalidIhos);
            }
        }
    }
}
