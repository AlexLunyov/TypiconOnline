using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Output;

namespace TypiconOnline.Domain.WebQuery.OutputFiltering
{
    [Serializable]
    [XmlRoot(OutputConstants.LocalizedOutputWeekNode)]
    public class FilteredOutputWeek
    {
        [XmlElement(OutputConstants.LocalizedOutputWeekNameNode)]
        public ItemTextUnit Name { get; set; }

        [XmlElement(OutputConstants.LocalizedOutputDayNode)]
        public List<FilteredOutputDay> Days { get; set; } = new List<FilteredOutputDay>();
    }
}
