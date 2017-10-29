using System;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
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
        [XmlAttribute(RuleConstants.MakarismiOdiNumberAttr)]
        public int Number { get; set; }
        /// <summary>
        /// Количество стихов
        /// </summary>
        [XmlAttribute(RuleConstants.MakarismiOdiCountAttr)]
        public int Count { get; set; }

        #endregion

        protected override void Validate()
        {
            if ((Number < 1) || (Number > 9))
            {
                //номер песни должен иметь значения с 1 до 9
                AddBrokenConstraint(OdiBusinessConstraint.InvalidNumber, RuleConstants.KanonasOdiNode);
            }

            if (Count < 0)
            {
                //количество тропарей должно иметь положительное значение
                AddBrokenConstraint(MakariosOdiBusinessConstraint.InvalidNumber, RuleConstants.KanonasOdiNode); 
            }
        }
    }
}