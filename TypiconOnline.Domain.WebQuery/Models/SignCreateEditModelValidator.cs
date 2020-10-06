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
    public class SignCreateEditModelValidator : TemplateRuleModelValidatorBase<SignCreateEditModel>
    {
        const string DefaultLanguage = "cs-ru";
        public SignCreateEditModelValidator(IRuleSerializerRoot ruleSerializer) : base(ruleSerializer) { }

        public override IEnumerable<ValidationResult> Validate(SignCreateEditModel model)
        {
            var errors = ValidateModRule(model);

            if (string.IsNullOrEmpty(model.Name))
            {
                errors.Add(new ValidationResult($"Наименование обязательно для заполнения", new List<string>() { "Name" }));
            }

            if (model.TemplateId < 1 && model.PrintTemplateId < 1)
            {
                errors.Add(new ValidationResult("Если Ссылка на Шаблон не определена, то должен быть указан Печатный шаблон", new List<string>() { "PrintTemplateId" }));
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
