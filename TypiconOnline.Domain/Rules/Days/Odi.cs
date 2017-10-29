using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Песнь канона
    /// </summary>
    [Serializable]
    public class Odi : DayElementBase
    {
        public Odi()
        {
            TroparionCollection = new List<Ymnos>();
        }

        #region Properties

        /// <summary>
        /// Глас
        /// </summary>
        [XmlAttribute(RuleConstants.OdiNumberAttrName)]
        public int Number { get; set; }

        /// <summary>
        /// Ирмос
        /// </summary>
        [XmlElement(RuleConstants.OdiIrmosNode)]
        public Ymnos Irmos { get; set; }
        /// <summary>
        /// Тропари песни канона
        /// </summary>
        [XmlElement(RuleConstants.OdiTroparionName)]
        public List<Ymnos> TroparionCollection { get; set; }
        /// <summary>
        /// Ирмос
        /// </summary>
        [XmlElement(RuleConstants.OdiKatavasiaNode)]
        public ItemText Katavasia { get; set; }

        #endregion

        protected override void Validate()
        {
            if ((Number < 1) || (Number > 9))
            {
                //номер песни должен иметь значения с 1 до 9
                AddBrokenConstraint(OdiBusinessConstraint.InvalidNumber, RuleConstants.KanonasOdiNode);
            }

            if (Irmos == null)
            {
                AddBrokenConstraint(OdiBusinessConstraint.IrmosRequired);
            }
            else if (!Irmos.IsValid)
            {
                AppendAllBrokenConstraints(Irmos, RuleConstants.OdiIrmosNode);
            }

            foreach (Ymnos trop in TroparionCollection)
            {
                if (!trop.IsValid)
                {
                    AppendAllBrokenConstraints(trop, RuleConstants.OdiTroparionName);
                }
            }

            if (Katavasia?.IsValid == false)
            {
                AppendAllBrokenConstraints(Katavasia, RuleConstants.OdiKatavasiaNode);
            }
        }
    }
}
