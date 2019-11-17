using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class DayRuleModelValidatorBase<T, U> : TemplateRuleModelValidatorBase<T> where T : class, new()
    {
        public DayRuleModelValidatorBase(IRuleSerializerRoot ruleSerializer) : base(ruleSerializer) { }

        protected List<ValidationResult> ValidateDayRule(DayRuleModelBase<U> model)
        {
            var errors = ValidateModRule(model);

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

            return errors;
        }
    }
}
