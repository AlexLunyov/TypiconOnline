using System;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{

    [Serializable]
    public class EvangelionPart : ValueObjectBase
    {
        public EvangelionPart() { }

        public EvangelionPart(XmlNode node)
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

            //номер
            XmlAttribute ihosAttr = node.Attributes[RuleConstants.EvangelionPartNumberAttr];
            if (ihosAttr != null)
            {
                int result = default(int);
                int.TryParse(ihosAttr.Value, out result);
                Number = result;
            }
        }

        #region Properties

        /// <summary>
        /// Наименование книги
        /// </summary>
        [XmlAttribute(RuleConstants.EvangelionBookNameAttr)]
        public EvangelionBook BookName { get; set; }

        /// <summary>
        /// Ноемр зачала
        /// </summary>
        [XmlAttribute(RuleConstants.EvangelionPartNumberAttr)]
        public int Number { get; set; }

        #endregion

        protected override void Validate()
        {
            if (Number < 0)
            {
                AddBrokenConstraint(EvangelionPartBusinessConstraint.InvalidNumber);
            }
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