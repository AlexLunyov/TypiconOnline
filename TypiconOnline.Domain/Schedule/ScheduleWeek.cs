using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Schedule
{
    public class ScheduleWeek
    {
        public ScheduleWeek()
        {
            Days = new List<ScheduleDay>();
        }

        public string Name { get; set; }
        public List<ScheduleDay> Days { get; set; }
    }
}
