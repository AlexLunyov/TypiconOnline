using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Domain.Typicon
{
    public class TriodionRule : DayRule
    {
        public virtual int DaysFromEaster { get; set; }
    }
}
