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

            if (string.IsNullOrEmpty(model.Name))
            {
                errors.Add(new ValidationResult($"Наименование обязательно для заполнения", new List<string>() { nameof(model.Name) }));
            }

            if (model.NameStyle == null)
            {
                errors.Add(new ValidationResult($"Стиль отображения дня не определен", new List<string>() { nameof(model.NameStyle) }));
            }

            return errors;
        }
    }
}
