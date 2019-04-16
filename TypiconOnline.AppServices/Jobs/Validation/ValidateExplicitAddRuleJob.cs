using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Jobs.Validation
{
    public class ValidateExplicitAddRuleJob : ValidateRuleJob<ExplicitAddRule>
    {
        public ValidateExplicitAddRuleJob(int id) : base(id) { }
    }
}
