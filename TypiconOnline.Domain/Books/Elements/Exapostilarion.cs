using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Описание эксапостилария, находящегося после 9-й песни канона утрени
    /// </summary>
    [Serializable]
    public class Exapostilarion : DayElementBase, IMergable<Exapostilarion>
    {
        #region Properties

        /// <summary>
        /// Коллекция песнопений
        /// </summary>
        [XmlElement(ElementConstants.ExapostilarionYmnosNode)]
        public List<ExapostilarionItem> Ymnis { get; set; } = new List<ExapostilarionItem>();

        /// <summary>
        /// Богородичен
        /// </summary>
        [XmlElement(ElementConstants.ExapostilarionTheotokionNode)]
        public ExapostilarionItem Theotokion { get; set; }

        #endregion

        protected override void Validate()
        {
            //nothing yet
        }

        /// <summary>
        /// Производит слияние двух Структур песнопений. Славник и Богородичен переписывается
        /// </summary>
        /// <param name="structure"></param>
        /// <param name="source"></param>
        public void Merge(Exapostilarion source)
        {
            if (source == null)
            {
                return;
            }

            Ymnis.AddRange(source.Ymnis);

            if (source.Theotokion != null)
            {
                Theotokion = source.Theotokion;
            }
        }
    }
}
