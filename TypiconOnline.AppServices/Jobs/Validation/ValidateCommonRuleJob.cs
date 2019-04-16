using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Jobs.Validation
{
    public class ValidateCommonRuleJob : ValidateRuleJob<CommonRule>
    {
        public ValidateCommonRuleJob(int id) : base(id) { }
    }
}
