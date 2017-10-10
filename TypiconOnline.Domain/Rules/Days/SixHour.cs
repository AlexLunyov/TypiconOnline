using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    [Serializable]
    [XmlRoot(ElementName = RuleConstants.SixHourNode)]
    public class SixHour : DayElementBase
    {
        public SixHour() { }

        /// <summary>
        /// Тропарь пророчества
        /// </summary>
        [XmlElement(RuleConstants.SixHourTroparionNode)]
        public YmnosStructure Troparion { get; set; }
        /// <summary>
        /// Прокимны/
        /// </summary>
        [XmlArray(RuleConstants.ProkeimeniNode)]
        [XmlArrayItem(RuleConstants.ProkeimenonNode)]
        public List<Prokeimenon> Prokeimeni { get; set; }
        /// <summary>
        /// Паремии
        /// </summary>
        [XmlArray(RuleConstants.ParoimiesNode)]
        [XmlArrayItem(RuleConstants.ParoimiaNode)]
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