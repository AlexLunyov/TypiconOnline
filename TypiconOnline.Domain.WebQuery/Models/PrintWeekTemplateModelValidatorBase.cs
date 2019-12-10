using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using TypiconOnline.AppServices.Viewers;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.AppServices.Extensions;
using DocumentFormat.OpenXml;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class PrintWeekTemplateModelValidatorBase<T> : RuleSerializerModelValidatorBase<T> where T : class, new()
    {
        const int MIN_DAYS = 1;
        const int MAX_DAYS = 7;

        const int MAX_FILE_SIZE = 500 * 1024;

        readonly string[] EXTENSIONS = { ".docx" };

        public PrintWeekTemplateModelValidatorBase(IRuleSerializerRoot ruleSerializer)
            : base(ruleSerializer) { }

        protected List<ValidationResult> ValidatePrintWeekTemplate(PrintWeekTemplateModelBase model)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (model.DaysPerPage < MIN_DAYS || model.DaysPerPage > MAX_DAYS)
            {
                errors.Add(new ValidationResult($"Количество дней должно быть определено в интервале {MIN_DAYS}..{MAX_DAYS}", new List<string>() { "DaysPerPage" }));
            }

            if (model.File != null)
            {
                //расширение
                var extension = Path.GetExtension(model.File.FileName);

                if (!EXTENSIONS.Contains(extension.ToLower()))
                {
                    errors.Add(new ValidationResult($"Разрешение файла {extension} недопустимо", new List<string>() { "File" }));
                }

                //размер
                if (model.File.Length > MAX_FILE_SIZE)
                {
                    errors.Add(new ValidationResult($"Максимально допустимый размер файла составляет { MAX_FILE_SIZE} байтов.", new List<string>() { "File" }));
                }

                //проверка полей для заполнения

                // создаем word документ
                try
                {
                    using (WordprocessingDocument weekDoc = WordprocessingDocument.Open(model.File.OpenReadStream(), false))
                    {
                        //[таблица]
                        var datePlacements = weekDoc.MainDocumentPart.Document.Body
                            .FindElementsByText(OutputTemplateConstants.DaysPlacement);

                        Text datePlacement = null;

                        if (datePlacements.Count() == 0)
                        {
                            errors.Add(new ValidationResult($"Поле {OutputTemplateConstants.DaysPlacement} отсутствует в определении шаблона", new List<string>() { "File" }));
                        }
                        else if (datePlacements.Count() > 1)
                        {
                            errors.Add(new ValidationResult($"Поле {OutputTemplateConstants.DaysPlacement} должно быть определено единожды в шаблоне", new List<string>() { "File" }));
                        }
                        else
                        {
                            datePlacement = datePlacements.First();
                        }
                    }
                }
                catch (Exception ex)
                    when (ex is IOException
                       || ex is ArgumentNullException
                       || ex is OpenXmlPackageException)
                {
                    errors.Add(new ValidationResult($"Неверный формат файла. Произошла ошибка при чтении", new List<string>() { "File" }));
                }
            }

            return errors;
        }
    }
}
