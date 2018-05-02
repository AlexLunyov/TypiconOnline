using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.ViewModels
{
    [Serializable]
    [XmlRoot(ViewModelConstants.ScheduleWeekNode)]
    public class ScheduleWeek
    {
        [XmlElement(ViewModelConstants.ScheduleWeekNameNode)]
        public ItemTextUnit Name { get; set; }

        [XmlElement(ViewModelConstants.ScheduleDayNode)]
        public List<ScheduleDay> Days { get; set; } = new List<ScheduleDay>();
    }
}
