using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Output
{
    [Serializable]
    [XmlRoot(OutputConstants.LocalizedOutputWeekNode)]
    public class LocalizedOutputWeek
    {
        [XmlElement(OutputConstants.LocalizedOutputWeekNameNode)]
        public ItemTextUnit Name { get; set; }

        [XmlElement(OutputConstants.LocalizedOutputDayNode)]
        public List<LocalizedOutputDay> Days { get; set; } = new List<LocalizedOutputDay>();
    }
}
