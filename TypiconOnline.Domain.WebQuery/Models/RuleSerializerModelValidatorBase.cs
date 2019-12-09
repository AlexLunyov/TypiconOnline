using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class RuleSerializerModelValidatorBase<T> : ModelValidatorBase<T> where T : class, new()
    {
        public RuleSerializerModelValidatorBase(IRuleSerializerRoot ruleSerializer)
        {
            RuleSerializer = ruleSerializer ?? throw new ArgumentNullException(nameof(ruleSerializer));
        }

        protected IRuleSerializerRoot RuleSerializer { get; }
    }
}
