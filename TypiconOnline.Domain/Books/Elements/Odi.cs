using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Books.Elements
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
        [XmlAttribute(ElementConstants.OdiNumberAttrName)]
        public int Number { get; set; }

        /// <summary>
        /// Тропари песни канона
        /// </summary>
        [XmlElement(ElementConstants.OdiTroparionName)]
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
                AddBrokenConstraint(OdiBusinessConstraint.InvalidNumber, ElementConstants.KanonasOdiNode);
            }

            //тропари должны быть
            if (Troparia.Count == 0)
            {
                AddBrokenConstraint(OdiBusinessConstraint.TroparionRequired, ElementConstants.KanonasOdiNode);
            }
            else
            {
                foreach (Ymnos trop in Troparia)
                {
                    if (!trop.IsValid)
                    {
                        AppendAllBrokenConstraints(trop, ElementConstants.OdiTroparionName);
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
