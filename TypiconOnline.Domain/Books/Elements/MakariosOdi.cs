using System;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Описание песни из блаженн
    /// </summary>
    [Serializable]
    public class MakariosOdi : DayElementBase
    {
        public MakariosOdi() { }

        #region Properties
        /// <summary>
        /// Номер песни
        /// </summary>
        [XmlAttribute(ElementConstants.MakarismiOdiNumberAttr)]
        public int Number { get; set; }
        /// <summary>
        /// Количество стихов
        /// </summary>
        [XmlAttribute(ElementConstants.MakarismiOdiCountAttr)]
        public int Count { get; set; }

        #endregion

        protected override void Validate()
        {
            if ((Number < 1) || (Number > 9))
            {
                //номер песни должен иметь значения с 1 до 9
                AddBrokenConstraint(OdiBusinessConstraint.InvalidNumber, ElementConstants.KanonasOdiNode);
            }

            if (Count < 0)
            {
                //количество тропарей должно иметь положительное значение
                AddBrokenConstraint(MakariosOdiBusinessConstraint.InvalidNumber, ElementConstants.KanonasOdiNode); 
            }
        }
    }
}