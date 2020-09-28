namespace TypiconOnline.Domain.Typicon
{
    public class SignSchedule
    {
        public int SignId { get; set; }
        public virtual Sign Sign { get; set; }
        public int ScheduleSettingsId { get; set; }
        public virtual ScheduleSettings ScheduleSettings { get; set; }
    }
}