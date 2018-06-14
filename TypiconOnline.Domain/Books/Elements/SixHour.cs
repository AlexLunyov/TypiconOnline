using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Books.Elements
{
    [Serializable]
    [XmlRoot(ElementName = ElementConstants.SixHourNode)]
    public class SixHour : DayElementBase
    {
        public SixHour() { }

        /// <summary>
        /// Тропарь пророчества
        /// </summary>
        [XmlElement(ElementConstants.SixHourTroparionNode)]
        public YmnosStructure Troparion { get; set; }
        /// <summary>
        /// Прокимны/
        /// </summary>
        [XmlArray(ElementConstants.ProkeimeniNode)]
        [XmlArrayItem(ElementConstants.ProkeimenonNode)]
        public List<Prokeimenon> Prokeimeni { get; set; }
        /// <summary>
        /// Паремии
        /// </summary>
        [XmlArray(ElementConstants.ParoimiesNode)]
        [XmlArrayItem(ElementConstants.ParoimiaNode)]
        public List<Paroimia> Paroimies { get; set; }

        protected override void Validate()
        {
            if (Troparion == null || Troparion.YmnosStructureCount == 0)
            {
                AddBrokenConstraint(SixHourBusinessConstraint.TroparionRequired);
            }

            if (Prokeimeni == null || Prokeimeni.Count == 0)
            {
                AddBrokenConstraint(SixHourBusinessConstraint.ProkeimenRequired);
            }

            if (Paroimies == null || Paroimies.Count == 0)
            {
                AddBrokenConstraint(SixHourBusinessConstraint.ParoimiaRequired);
            }
        }
    }
}