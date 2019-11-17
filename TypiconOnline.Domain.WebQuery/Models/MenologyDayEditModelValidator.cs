using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class MenologyDayEditModelValidator : DayBookModelValidatorBase<MenologyDayEditModel>
    {
        public MenologyDayEditModelValidator(ITypiconSerializer serializer) : base(serializer) { }

        public override IEnumerable<ValidationResult> Validate(MenologyDayEditModel model)
        {
            List<ValidationResult> errors = ValidateDay(model);

            if (model.LeapDate.HasValue
                && model.LeapDate.Value == DateTime.MinValue)
            {
                errors.Add(new ValidationResult($"Недопустимое значение даты", new List<string>() { "LeapDate" }));
            }


            return errors;
        }
    }
}
