using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class DayRuleModelValidatorBase<T>
    {
        public DayRuleModelValidatorBase(IRuleSerializerRoot ruleSerializer)
        {
            RuleSerializer = ruleSerializer ?? throw new ArgumentNullException(nameof(ruleSerializer));
        }

        protected IRuleSerializerRoot RuleSerializer { get; }

        protected List<ValidationResult> ValidateDayRule(DayRuleModelBase<T> model)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (model.DayWorships == null || model.DayWorships.Count < 1)
            {
                errors.Add(new ValidationResult("Список Текстов служб не может быть пустым", new List<string>() { "DayWorships" }));
            }

            if (model.DayWorships?.Count > 3)
            {
                errors.Add(new ValidationResult("Список Текстов служб не может иметь более 3х элементов", new List<string>() { "DayWorships" }));
            }

            if (model.TemplateId < 1)
            {
                errors.Add(new ValidationResult("Шаблон для Правила должен быть определен", new List<string>() { "TemplateId" }));
            }

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
