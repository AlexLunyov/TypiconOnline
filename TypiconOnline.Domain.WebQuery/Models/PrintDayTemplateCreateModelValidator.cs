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

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class PrintDayTemplateCreateModelValidator : PrintDayTemplateModelValidatorBase<PrintDayTemplateCreateModel>
    {
        public PrintDayTemplateCreateModelValidator(IRuleSerializerRoot ruleSerializer) : base(ruleSerializer) { }

        public override IEnumerable<ValidationResult> Validate(PrintDayTemplateCreateModel model)
        {
            List<ValidationResult> errors = ValidatePrintDayTemplate(model);

            if (model.File == null)
            {
                errors.Add(new ValidationResult($"Файл шаблона обязателен для загрузки", new List<string>() { "File" }));
            }

            return errors;
        }
    }
}
