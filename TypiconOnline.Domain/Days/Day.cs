
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Days
{
    public abstract class Day : RuleEntity/*<CalendarContainer>*/
    {
        public virtual ItemText Name1 { get; set; }
        public virtual ItemText Name2 { get; set; }
        public virtual ItemText Name3 { get; set; }
    }
}

