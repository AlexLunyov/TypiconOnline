using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Jobs.Validation
{
    public abstract class ValidateRuleJob<T>: IJob where T : RuleEntity, new()
    {
        public ValidateRuleJob(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public bool Equals(IJob other) => other is ValidateRuleJob<T> j && Id == j.Id;
    }
}
