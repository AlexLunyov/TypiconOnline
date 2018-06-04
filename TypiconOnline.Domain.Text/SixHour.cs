using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    [Serializable]
    [XmlRoot(ElementName = XmlConstants.SixHourNode)]
    public class SixHour : DayElementBase
    {
        public SixHour() { }

        /// <summary>
        /// Тропарь пророчества
        /// </summary>
        [XmlElement(XmlConstants.SixHourTroparionNode)]
        public YmnosStructure Troparion { get; set; }
        /// <summary>
        /// Прокимны/
        /// </summary>
        [XmlArray(XmlConstants.ProkeimeniNode)]
        [XmlArrayItem(XmlConstants.ProkeimenonNode)]
        public List<Prokeimenon> Prokeimeni { get; set; }
        /// <summary>
        /// Паремии
        /// </summary>
        [XmlArray(XmlConstants.ParoimiesNode)]
        [XmlArrayItem(XmlConstants.ParoimiaNode)]
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