using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Core
{
    /// <summary>
    /// Класс соединения правил и дневных служб
    /// </summary>
    public class DayRuleWorship 
    {
        public int DayRuleId { get; set; }
        public virtual DayRule DayRule { get; set; }
        public int DayWorshipId { get; set; }
        public virtual DayWorship DayWorship { get; set; }
    }
}