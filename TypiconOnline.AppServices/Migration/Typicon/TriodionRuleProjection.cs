using System;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    [Serializable]
    public class TriodionRuleProjection : DayRuleProjection
    {
        public TriodionRuleProjection() { }
        public int DaysFromEaster { get; set; }
        public bool IsTransparent { get; set; }
    }
}