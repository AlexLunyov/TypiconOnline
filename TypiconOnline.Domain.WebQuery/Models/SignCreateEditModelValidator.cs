using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class SignCreateEditModelValidator : TemplateRuleModelValidatorBase, IValidator<SignCreateEditModel>
    {
        const string DefaultLanguage = "cs-ru";
        public SignCreateEditModelValidator(IRuleSerializerRoot ruleSerializer) : base(ruleSerializer) { }

        public IEnumerable<ValidationResult> Validate(SignCreateEditModel model)
        {
            var errors = ValidateModRule(model);

            if (model.Name == null
                || !model.Name.Languages.Contains(DefaultLanguage))
            {
                errors.Add(new ValidationResult($"Наименование должно быть определено на языке \"{DefaultLanguage}\"", new List<string>() { "Name" }));
            }

            if (model.Name?.IsValid == false)
            {
                errors.Add(new ValidationResult($"Наименование заполнено с неверным определением языка", new List<string>() { "Name" }));
            }

            if (model.Number.HasValue
                && (model.Number.Value < 0 || model.Number.Value > 8))
            {
                errors.Add(new ValidationResult("Предустановленный номер должен быть определн в диапазоне (0..8)", new List<string>() { "Number" }));
            }

            if (model.Priority < 1 || model.Priority > 5)
            {
                errors.Add(new ValidationResult("Приоритет должен быть определн в диапазоне (1..5)", new List<string>() { "Priority" }));
            }

            if (model.TemplateId < 1 && string.IsNullOrEmpty(model.RuleDefinition))
            {
                errors.Add(new ValidationResult("Если Отсутствует ссылка на Шаблон, Правило последовательности должно быть определено", new List<string>() { "RuleDefinition" }));
            }

            return errors;
        }
    }
}
