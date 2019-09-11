using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Typicon.Output
{
    public class OutputDayWorship
    {
        public int OutputDayId { get; set; }
        public virtual OutputDay OutputDay { get; set; }
        public int DayWorshipId { get; set; }
        public virtual DayWorship DayWorship { get; set; }
    }
}
