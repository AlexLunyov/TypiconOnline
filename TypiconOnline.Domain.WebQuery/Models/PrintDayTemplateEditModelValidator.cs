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
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class PrintDayTemplateEditModelValidator : PrintDayTemplateModelValidatorBase<PrintDayTemplateEditModel>
    {
        public PrintDayTemplateEditModelValidator(IRuleSerializerRoot ruleSerializer, TypiconDBContext dbContext)
            : base(ruleSerializer, dbContext) { }

        public override IEnumerable<ValidationResult> Validate(PrintDayTemplateEditModel model)
        {
            var errors = ValidatePrintDayTemplate(model);

            //проверяем на уникальность номера
            //сначала находим сам редактируемый объект
            var found = DbContext.Set<PrintDayTemplate>().FirstOrDefault(c => c.Id == model.Id);

            if (found?.Number == model.Number)
            {
                //если значения Number найденного объекта и редактируемой модели равны,
                //значит ошибки нет
                found = null;
            }
            else if (found != null)
            {
                //ищем дальше
                found = DbContext.Set<PrintDayTemplate>()
                    .FirstOrDefault(c => c.TypiconVersionId == found.TypiconVersionId
                        && c.Number == model.Number);
            }

            if (found != null)
            {
                errors.Add(new ValidationResult($"Номер шаблона должен быть уникальным", new List<string>() { "Number" }));
            }

            return errors;
        }
         
    }
}
