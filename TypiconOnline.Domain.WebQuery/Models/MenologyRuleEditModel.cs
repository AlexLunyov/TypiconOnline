using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Infrastructure.Common.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class MenologyRuleEditModel : MenologyRuleModelBase
    {
        public DateTime? Date { get; set; }
        public DateTime? LeapDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (DayWorships == null || DayWorships.Count == 0)
            {
                errors.Add(new ValidationResult("Список Текстов служб не может быть пустым", new List<string>() { "DayWorships" }));
            }

            if (TemplateId < 1)
            {
                errors.Add(new ValidationResult("Шаблон для Правила должен быть определен", new List<string>() { "TemplateId" }));
            }

            //IRuleSerializerRoot ruleSerializer = validationContext.GetRequiredService<IRuleSerializerRoot>();

            //if (!string.IsNullOrEmpty(ModRuleDefinition))
            //{
            //    var modRule = ruleSerializer.Container<RootContainer>().Deserialize(ModRuleDefinition);

            //    if (modRule == null)
            //    {
            //        errors.Add(new ValidationResult("Правило для переноса служб заполнено с неопределяемыми системой ошибками", new List<string>() { "ModRuleDefinition" }));
            //    }
            //    else if (!modRule.IsValid)
            //    {
            //        errors.Add(new ValidationResult(modRule.GetBrokenConstraints().GetSummary(), new List<string>() { "ModRuleDefinition" }));
            //    }
            //}

            //if (!string.IsNullOrEmpty(RuleDefinition))
            //{
            //    var rule = ruleSerializer.Container<RootContainer>().Deserialize(RuleDefinition);

            //    if (rule == null)
            //    {
            //        errors.Add(new ValidationResult("Правило заполнено с неопределяемыми системой ошибками", new List<string>() { "RuleDefinition" }));
            //    }
            //    else if (!rule.IsValid)
            //    {
            //        errors.Add(new ValidationResult(rule.GetBrokenConstraints().GetSummary(), new List<string>() { "RuleDefinition" }));
            //    }
            //}

            return errors;
        }
    }
}
