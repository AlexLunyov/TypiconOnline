using System;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    [Serializable]
    public class CommonRuleProjection : RuleProjection
    {
        public CommonRuleProjection() { }
        public string Name { get; set; }
    }
}