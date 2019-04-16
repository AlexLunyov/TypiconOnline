using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Jobs.Validation
{
    public class ValidateTriodionRuleJob : ValidateRuleJob<TriodionRule>
    {
        public ValidateTriodionRuleJob(int id) : base(id) { }
    }
}
