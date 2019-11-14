using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class RuleModelValidatorBase
    {
        public RuleModelValidatorBase(IRuleSerializerRoot ruleSerializer)
        {
            RuleSerializer = ruleSerializer ?? throw new ArgumentNullException(nameof(ruleSerializer));
        }

        protected IRuleSerializerRoot RuleSerializer { get; }

        protected List<ValidationResult> ValidateRule(RuleModelBase model)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (!string.IsNullOrEmpty(model.RuleDefinition))
            {
                var rule = RuleSerializer.Container<RootContainer>().Deserialize(model.RuleDefinition);

                if (rule == null)
                {
                    errors.Add(new ValidationResult("Правило заполнено с неопределяемыми системой ошибками", new List<string>() { "RuleDefinition" }));
                }
                else if (!rule.IsValid)
                {
                    errors.Add(new ValidationResult(rule.GetBrokenConstraints().GetSummary(), new List<string>() { "RuleDefinition" }));
                }
            }

            return errors;
        }
    }
}
