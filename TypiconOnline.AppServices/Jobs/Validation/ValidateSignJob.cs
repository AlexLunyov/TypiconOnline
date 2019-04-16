using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Jobs.Validation
{
    public class ValidateSignJob : ValidateRuleJob<Sign>
    {
        public ValidateSignJob(int id) : base(id) { }
    }
}
