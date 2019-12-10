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

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class PrintDayTemplateCreateModelValidator : PrintDayTemplateModelValidatorBase<PrintDayTemplateCreateModel>
    {
        public PrintDayTemplateCreateModelValidator(IRuleSerializerRoot ruleSerializer, TypiconDBContext dbContext) 
            : base(ruleSerializer, dbContext) { }

        public override IEnumerable<ValidationResult> Validate(PrintDayTemplateCreateModel model)
        {
            List<ValidationResult> errors = ValidatePrintDayTemplate(model);

            if (model.File == null)
            {
                errors.Add(new ValidationResult($"Файл шаблона обязателен для загрузки", new List<string>() { "File" }));
            }

            //проверяем на уникальность
            //значит ищем CommonRule с таким же Name
            var found = DbContext.Set<PrintDayTemplate>()
                .FirstOrDefault(c => c.Number == model.Number
                                     && c.TypiconVersion.TypiconId == model.Id
                                     && c.TypiconVersion.BDate == null
                                     && c.TypiconVersion.EDate == null);

            if (found != null)
            {
                errors.Add(new ValidationResult($"Номер шаблона должен быть уникальным", new List<string>() { "Number" }));
            }

            return errors;
        }
    }
}
