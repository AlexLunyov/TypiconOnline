using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class TriodionDayEditModelValidator : DayBookModelValidatorBase<TriodionDayEditModel>
    {
        public TriodionDayEditModelValidator(ITypiconSerializer serializer) : base(serializer) { }

        public override IEnumerable<ValidationResult> Validate(TriodionDayEditModel model)
        {
            List<ValidationResult> errors = ValidateDay(model);

            if (model.DaysFromEaster < -363
               || model.DaysFromEaster > 363)
            {
                errors.Add(new ValidationResult("Количество дней от Пасхи должно варьироваться в пределах одного календарного года (-364..364)", new List<string>() { "DayWorships" }));
            }

            return errors;
        }
    }
}
