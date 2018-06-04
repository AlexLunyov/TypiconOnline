using System;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{

    [Serializable]
    public class EvangelionPart : ApostlesPart
    {
        public EvangelionPart() { }

        #region Properties

        /// <summary>
        /// Наименование книги
        /// </summary>
        [XmlAttribute(XmlConstants.EvangelionBookNameAttr)]
        public EvangelionBook BookName { get; set; }

        #endregion

        protected override void Validate()
        {
            base.Validate();
        }
    }

    public enum EvangelionBook
    {
        [XmlEnum(Name = XmlConstants.EvangelionBokMf)]
        Мф,
        [XmlEnum(Name = XmlConstants.EvangelionBokMk)]
        Мк,
        [XmlEnum(Name = XmlConstants.EvangelionBokLk)]
        Лк,
        [XmlEnum(Name = XmlConstants.EvangelionBokIn)]
        Ин
    }
}