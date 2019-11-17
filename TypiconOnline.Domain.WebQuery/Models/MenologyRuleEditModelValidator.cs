using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class MenologyRuleEditModelValidator : DayRuleModelValidatorBase<MenologyRuleEditModel, MenologyDayWorshipModel>
    {
        public MenologyRuleEditModelValidator(IRuleSerializerRoot ruleSerializer) : base(ruleSerializer) { }

        public override IEnumerable<ValidationResult> Validate(MenologyRuleEditModel model)
        {
            var errors = ValidateDayRule(model);

            if (model.Date == null && model.LeapDate == null
                && string.IsNullOrEmpty(model.ModRuleDefinition))
            {
                errors.Add(new ValidationResult("Правило для переноса служб должно быть определено, если Правило Минеи является переходящим праздником", new List<string>() { "ModRuleDefinition" }));
            }

            return errors;
        }
    }
}
