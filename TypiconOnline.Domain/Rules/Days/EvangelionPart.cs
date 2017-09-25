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

        public EvangelionPart(XmlNode node) : base(node)
        {
            //BookName
            XmlAttribute bookNameAttr = node.Attributes[RuleConstants.EvangelionBookNameAttr];
            if (bookNameAttr != null)
            {
                EvangelionBook book;

                if (Enum.TryParse(bookNameAttr.Value, out book))
                {
                    BookName = book;
                }
            }
        }

        #region Properties

        /// <summary>
        /// Наименование книги
        /// </summary>
        [XmlAttribute(RuleConstants.EvangelionBookNameAttr)]
        public EvangelionBook BookName { get; set; }

        #endregion

        protected override void Validate()
        {
            base.Validate();
        }
    }

    public enum EvangelionBook
    {
        [XmlEnum(Name = RuleConstants.EvangelionBokMf)]
        Мф,
        [XmlEnum(Name = RuleConstants.EvangelionBokMk)]
        Мк,
        [XmlEnum(Name = RuleConstants.EvangelionBokLk)]
        Лк,
        [XmlEnum(Name = RuleConstants.EvangelionBokIn)]
        Ин
    }
}