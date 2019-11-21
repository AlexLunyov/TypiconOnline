using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    public abstract class TemplateHavingRuleProjection: ModRuleProjection
    {
        public int? TemplateId { get; set; }
    }
}
