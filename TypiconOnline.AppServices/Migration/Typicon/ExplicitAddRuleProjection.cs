using System;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    [Serializable]
    public class ExplicitAddRuleProjection: RuleProjection
    {
        public ExplicitAddRuleProjection() { }
        public DateTime Date { get; set; }
    }
}