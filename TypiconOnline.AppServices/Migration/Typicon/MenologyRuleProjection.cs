using System;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    [Serializable]
    public class MenologyRuleProjection: DayRuleProjection
    {
        public MenologyRuleProjection() { }
        public string Date { get; set; }
        public string LeapDate { get; set; }
    }
}