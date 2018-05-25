﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
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
        [XmlElement(RuleConstants.ExapostilarionYmnosNode)]
        public List<ExapostilarionItem> Ymnis { get; set; } = new List<ExapostilarionItem>();

        /// <summary>
        /// Богородичен
        /// </summary>
        [XmlElement(RuleConstants.ExapostilarionTheotokionNode)]
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
