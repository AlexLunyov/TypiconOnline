using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain
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