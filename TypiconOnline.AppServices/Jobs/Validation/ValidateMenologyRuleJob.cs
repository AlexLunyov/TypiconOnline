using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Jobs.Validation
{
    public class ValidateMenologyRuleJob : ValidateRuleJob<MenologyRule>
    {
        public ValidateMenologyRuleJob(int id) : base(id) { }
    }
}
