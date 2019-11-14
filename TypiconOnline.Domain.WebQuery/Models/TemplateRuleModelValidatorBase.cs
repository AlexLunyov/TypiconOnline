using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class TemplateRuleModelValidatorBase : RuleModelValidatorBase
    {
        public TemplateRuleModelValidatorBase(IRuleSerializerRoot ruleSerializer) : base(ruleSerializer) { }


        protected List<ValidationResult> ValidateModRule(TemplateRuleModelBase model)
        {
            var errors = ValidateRule(model);

            if (!string.IsNullOrEmpty(model.ModRuleDefinition))
            {
                var modRule = RuleSerializer.Container<RootContainer>().Deserialize(model.ModRuleDefinition);

                if (modRule == null)
                {
                    errors.Add(new ValidationResult("Правило для переноса служб заполнено с неопределяемыми системой ошибками", new List<string>() { "ModRuleDefinition" }));
                }
                else if (!modRule.IsValid)
                {
                    errors.Add(new ValidationResult(modRule.GetBrokenConstraints().GetSummary(), new List<string>() { "ModRuleDefinition" }));
                }
            }

            return errors;
        }
    }
}
