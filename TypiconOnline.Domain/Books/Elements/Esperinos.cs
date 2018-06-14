using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Вечерня
    /// </summary>
    [Serializable]
    public class Esperinos : MikrosEsperinos
    {
        public Esperinos() { }

        #region Properties

        /// <summary>
        /// Прокимны на вечерне.
        /// Два прокимна бывает Великим постом
        /// </summary>
        [XmlArray(ElementConstants.ProkeimeniNode)]
        [XmlArrayItem(ElementConstants.ProkeimenonNode)]
        public List<Prokeimenon> Prokeimeni { get; set; }
        /// <summary>
        /// Список Паремий на Вечерне
        /// </summary>
        [XmlArray(ElementConstants.ParoimiesNode)]
        [XmlArrayItem(ElementConstants.ParoimiaNode)]
        public List<Paroimia> Paroimies { get; set; }
        /// <summary>
        /// Стихиры на литии
        /// </summary>
        [XmlElement(ElementConstants.LitiNode)]
        public YmnosStructure Liti { get; set; }
        

        #endregion

        protected override void Validate()
        {
            base.Validate();

            if (Prokeimeni != null)
            {
                Prokeimeni.ForEach(c =>
                {
                    if (!c.IsValid)
                    {
                        AppendAllBrokenConstraints(c, /*ElementName + "." + */ElementConstants.ProkeimenonNode);
                    }
                });
            }

            if (Liti?.IsValid == false)
            {
                AppendAllBrokenConstraints(Liti, /*ElementName + "." + */ElementConstants.LitiNode);
            }
        }
    }
}
