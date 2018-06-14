using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Books.Elements
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
        //[XmlArray(ElementConstants.MakarismiNode)]
        //[XmlArrayItem(ElementConstants.MakarismiOdiNode)]
        //public List<MakariosOdi> Makarismi { get; set; }

        /// <summary>
        /// Блаженны
        /// </summary>
        [XmlElement(ElementConstants.MakarismiNode)]
        public Makarismi Makarismi { get; set; }

        /// <summary>
        /// Прокимен
        /// </summary>
        [XmlElement(ElementConstants.ProkeimenonNode)]
        public List<Prokeimenon> Prokeimeni { get; set; }

        /// <summary>
        /// Апостольские чтения
        /// </summary>
        [XmlArray(ElementConstants.ApostlesNode)]
        [XmlArrayItem(ElementConstants.EvangelionPartNode)]
        public List<ApostlesPart> Apostles { get; set; }

        /// <summary>
        /// Аллилуиа
        /// </summary>
        [XmlElement(ElementConstants.AlleluiaNode)]
        public List<Prokeimenon> Alleluias { get; set; }

        /// <summary>
        /// Евангельские чтения
        /// </summary>
        [XmlArray(ElementConstants.EvangelionNode)]
        [XmlArrayItem(ElementConstants.EvangelionPartNode)]
        public List<EvangelionPart> Evangelion { get; set; }

        /// <summary>
        /// Причастен
        /// </summary>
        [XmlElement(ElementConstants.KinonikNode)]
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
                        AppendAllBrokenConstraints(odi, ElementConstants.MakarismiNode);
                    }
                }

                if (Makarismi.Ymnis?.IsValid == false)
                {
                    AppendAllBrokenConstraints(Makarismi.Ymnis, ElementConstants.MakarismiNode);
                }
            }

            if (Prokeimeni != null)
            {
                foreach (var prokeimenon in Prokeimeni)
                {
                    if (!prokeimenon.IsValid)
                    {
                        AppendAllBrokenConstraints(prokeimenon, ElementConstants.ProkeimenonNode);
                    }
                }
            }


            if (Alleluias != null)
            {
                foreach (var alleluia in Alleluias)
                {
                    if (!alleluia.IsValid)
                    {
                        AppendAllBrokenConstraints(alleluia, ElementConstants.AlleluiaNode);
                    }
                }
            }

            if (Apostles != null)
            {
                foreach (ApostlesPart part in Apostles)
                {
                    if (!part.IsValid)
                    {
                        AppendAllBrokenConstraints(part, ElementConstants.ApostlesNode);
                    }
                }
            }

            if (Evangelion != null)
            {
                foreach (EvangelionPart part in Evangelion)
                {
                    if (!part.IsValid)
                    {
                        AppendAllBrokenConstraints(part, ElementConstants.EvangelionNode);
                    }
                }
            }

            if (Kinoniki != null)
            {
                foreach (var kinonik in Kinoniki)
                {
                    if (!kinonik.IsValid)
                    {
                        AppendAllBrokenConstraints(kinonik, ElementConstants.KinonikNode);
                    }
                }
            }
        }
    }
}
