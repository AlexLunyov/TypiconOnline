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
            Troparia = new List<Ymnos>();
        }

        #region Properties

        /// <summary>
        /// Глас
        /// </summary>
        [XmlAttribute(RuleConstants.OdiNumberAttrName)]
        public int Number { get; set; }

        /// <summary>
        /// Тропари песни канона
        /// </summary>
        [XmlElement(RuleConstants.OdiTroparionName)]
        public List<Ymnos> Troparia { get; set; }

        //[XmlIgnore()]
        //public Ymnos Irmos
        //{
        //    get
        //    {
        //        return Troparia.FirstOrDefault(c => c.Kind == YmnosKind.Irmos);
        //    }
        //}

        //[XmlIgnore()]
        //public Ymnos Katavasia
        //{
        //    get
        //    {
        //        return Troparia.FirstOrDefault(c => c.Kind == YmnosKind.Katavasia);
        //    }
        //}

        #endregion

        protected override void Validate()
        {
            if ((Number < 1) || (Number > 9))
            {
                //номер песни должен иметь значения с 1 до 9
                AddBrokenConstraint(OdiBusinessConstraint.InvalidNumber, RuleConstants.KanonasOdiNode);
            }

            //тропари должны быть
            if (Troparia.Count == 0)
            {
                AddBrokenConstraint(OdiBusinessConstraint.TroparionRequired, RuleConstants.KanonasOdiNode);
            }
            else
            {
                foreach (Ymnos trop in Troparia)
                {
                    if (!trop.IsValid)
                    {
                        AppendAllBrokenConstraints(trop, RuleConstants.OdiTroparionName);
                    }
                }
            }

            //ирмос должен быть
            if (!Troparia.Exists(c => c.Kind == YmnosKind.Irmos))
            {
                AddBrokenConstraint(OdiBusinessConstraint.IrmosRequired);
            }
        }
    }
}
