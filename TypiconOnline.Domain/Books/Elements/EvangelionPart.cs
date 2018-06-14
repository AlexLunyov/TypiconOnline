using System;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Books.Elements
{

    [Serializable]
    public class EvangelionPart : ApostlesPart
    {
        public EvangelionPart() { }

        #region Properties

        /// <summary>
        /// Наименование книги
        /// </summary>
        [XmlAttribute(ElementConstants.EvangelionBookNameAttr)]
        public EvangelionBook BookName { get; set; }

        #endregion

        protected override void Validate()
        {
            base.Validate();
        }
    }

    public enum EvangelionBook
    {
        [XmlEnum(Name = ElementConstants.EvangelionBokMf)]
        Мф,
        [XmlEnum(Name = ElementConstants.EvangelionBokMk)]
        Мк,
        [XmlEnum(Name = ElementConstants.EvangelionBokLk)]
        Лк,
        [XmlEnum(Name = ElementConstants.EvangelionBokIn)]
        Ин
    }
}