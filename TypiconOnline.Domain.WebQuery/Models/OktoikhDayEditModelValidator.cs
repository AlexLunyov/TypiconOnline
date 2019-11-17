using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class OktoikhDayEditModelValidator : BookModelValidatorBase<OktoikhDayEditModel>
    {
        public OktoikhDayEditModelValidator(ITypiconSerializer serializer) : base(serializer) { }

        public override IEnumerable<ValidationResult> Validate(OktoikhDayEditModel model) => ValidateBook(model);
    }
}
