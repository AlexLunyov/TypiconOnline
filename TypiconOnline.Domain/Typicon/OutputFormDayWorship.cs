using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Typicon
{
    public class OutputFormDayWorship
    {
        public int OutputFormId { get; set; }
        public virtual OutputForm OutputForm { get; set; }
        public int DayWorshipId { get; set; }
        public virtual DayWorship DayWorship { get; set; }
    }
}
