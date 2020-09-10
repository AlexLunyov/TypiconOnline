using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using TypiconOnline.AppServices.Viewers;
using TypiconOnline.Domain.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;
using TypiconOnline.AppServices.Extensions;
using DocumentFormat.OpenXml;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Domain.WebQuery.Extensions;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class OutputDayEditModelModelValidator : ModelValidatorBase<OutputDayEditModel>
    {
        public override IEnumerable<ValidationResult> Validate(OutputDayEditModel model)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            model.Name.ValidateRequired(nameof(model.Name), "Наименование", errors);

            return errors;
        }
    }
}
