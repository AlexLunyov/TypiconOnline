
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Factories;

namespace TypiconOnline.Domain.Days
{
    public abstract class Day : RuleEntity/*<CalendarContainer>*/
    {
        //public virtual ItemText Name1 { get; set; }
        //public virtual ItemText Name2 { get; set; }
        //public virtual ItemText Name3 { get; set; }

        public virtual ItemTextCollection DayName { get; set; }

        //public override string RuleDefinition
        //{
        //    set
        //    {
        //        _ruleDefinition = value;

        //        _rule = RuleDayFactory.CreateRuleContainer(_ruleDefinition);
        //    }
        //}
    }
}

