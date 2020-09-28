namespace TypiconOnline.Domain.Typicon
{
    public class ModRuleEntitySchedule<T> where T: ModRuleEntity, new()
    {
        public int RuleId { get; set; }
        public virtual T Rule { get; set; }
        public int ScheduleSettingsId { get; set; }
        public virtual ScheduleSettings ScheduleSettings { get; set; }
    }

}