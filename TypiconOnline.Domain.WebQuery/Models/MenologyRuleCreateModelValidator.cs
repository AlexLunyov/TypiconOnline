using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class MenologyRuleCreateModelValidator : DayRuleModelValidatorBase<MenologyRuleCreateModel, MenologyDayWorshipModel>
    {
        public MenologyRuleCreateModelValidator(IRuleSerializerRoot ruleSerializer) : base(ruleSerializer) { }

        public override IEnumerable<ValidationResult> Validate(MenologyRuleCreateModel model)
        {
            var errors = ValidateDayRule(model);

            if (string.IsNullOrEmpty(model.ModRuleDefinition))
            {
                errors.Add(new ValidationResult("Правило для переноса служб должно быть определено", new List<string>() { "ModRuleDefinition" }));
            }
            return errors;
        }
    }
}
