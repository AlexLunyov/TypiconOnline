﻿using System;
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
    /// Описание Литургийных чтений
    /// </summary>
    [Serializable]
    public class Leitourgia : DayElementBase
    {
        public Leitourgia() { }

        #region Properties

        ///// <summary>
        ///// Блаженны
        ///// </summary>
        //[XmlArray(RuleConstants.MakarismiNode)]
        //[XmlArrayItem(RuleConstants.MakarismiOdiNode)]
        //public List<MakariosOdi> Makarismi { get; set; }

        /// <summary>
        /// Блаженны
        /// </summary>
        [XmlElement(RuleConstants.MakarismiNode)]
        public Makarismi Makarismi { get; set; }

        /// <summary>
        /// Прокимен
        /// </summary>
        [XmlElement(RuleConstants.ProkeimenonNode)]
        public List<Prokeimenon> Prokeimeni { get; set; }

        /// <summary>
        /// Апостольские чтения
        /// </summary>
        [XmlArray(RuleConstants.ApostlesNode)]
        [XmlArrayItem(RuleConstants.EvangelionPartNode)]
        public List<ApostlesPart> Apostles { get; set; }

        /// <summary>
        /// Аллилуиа
        /// </summary>
        [XmlElement(RuleConstants.AlleluiaNode)]
        public List<Prokeimenon> Alleluias { get; set; }

        /// <summary>
        /// Евангельские чтения
        /// </summary>
        [XmlArray(RuleConstants.EvangelionNode)]
        [XmlArrayItem(RuleConstants.EvangelionPartNode)]
        public List<EvangelionPart> Evangelion { get; set; }

        /// <summary>
        /// Причастен
        /// </summary>
        [XmlElement(RuleConstants.KinonikNode)]
        public List<Kinonik> Kinoniki { get; set; }

        #endregion

        protected override void Validate()
        {
            if (Makarismi != null)
            {
                foreach (MakariosOdi odi in Makarismi.Links)
                {
                    if (!odi.IsValid)
                    {
                        AppendAllBrokenConstraints(odi, RuleConstants.MakarismiNode);
                    }
                }

                if (Makarismi.Ymnis?.IsValid == false)
                {
                    AppendAllBrokenConstraints(Makarismi.Ymnis, RuleConstants.MakarismiNode);
                }
            }

            if (Prokeimeni != null)
            {
                foreach (var prokeimenon in Prokeimeni)
                {
                    if (!prokeimenon.IsValid)
                    {
                        AppendAllBrokenConstraints(prokeimenon, RuleConstants.ProkeimenonNode);
                    }
                }
            }


            if (Alleluias != null)
            {
                foreach (var alleluia in Alleluias)
                {
                    if (!alleluia.IsValid)
                    {
                        AppendAllBrokenConstraints(alleluia, RuleConstants.AlleluiaNode);
                    }
                }
            }

            if (Apostles != null)
            {
                foreach (ApostlesPart part in Apostles)
                {
                    if (!part.IsValid)
                    {
                        AppendAllBrokenConstraints(part, RuleConstants.ApostlesNode);
                    }
                }
            }

            if (Evangelion != null)
            {
                foreach (EvangelionPart part in Evangelion)
                {
                    if (!part.IsValid)
                    {
                        AppendAllBrokenConstraints(part, RuleConstants.EvangelionNode);
                    }
                }
            }

            if (Kinoniki != null)
            {
                foreach (var kinonik in Kinoniki)
                {
                    if (!kinonik.IsValid)
                    {
                        AppendAllBrokenConstraints(kinonik, RuleConstants.KinonikNode);
                    }
                }
            }
        }
    }
}
