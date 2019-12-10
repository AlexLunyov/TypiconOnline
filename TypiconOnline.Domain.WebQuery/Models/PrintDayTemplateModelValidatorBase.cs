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
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class PrintDayTemplateModelValidatorBase<T> : RuleSerializerModelValidatorBase<T> where T : class, new()
    {
        const int MIN_NUMBER = 0;
        const int MAX_NUMBER = 100;

        const int MAX_FILE_SIZE = 500 * 1024;

        readonly string[] EXTENSIONS = { ".docx" };

        public PrintDayTemplateModelValidatorBase(IRuleSerializerRoot ruleSerializer, TypiconDBContext dbContext)
            : base(ruleSerializer)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected TypiconDBContext DbContext { get; }

        protected List<ValidationResult> ValidatePrintDayTemplate(PrintDayTemplateModelBase model)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(model.Name))
            {
                errors.Add(new ValidationResult($"Наименование должно быть определено", new List<string>() { "Name" }));
            }

            if (model.Number < MIN_NUMBER || model.Number > MAX_NUMBER)
            {
                errors.Add(new ValidationResult($"Номер должен быть опредеден в диапазоне {MIN_NUMBER}..{MAX_NUMBER}", new List<string>() { "Number" }));
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
                        //[дата]
                        var datePlacement = weekDoc.MainDocumentPart.Document.Body
                            .FindElementsByText(OutputTemplateConstants.Date)
                            .FirstOrDefault();

                        if (datePlacement == null)
                        {
                            errors.Add(new ValidationResult($"Поле {OutputTemplateConstants.Date} отсутствует в определении шаблона", new List<string>() { "File" }));
                        }

                        //[имядня]
                        var dayNamePlacement = weekDoc.MainDocumentPart.Document.Body
                            .FindElementsByText(OutputTemplateConstants.DayName)
                            .FirstOrDefault();

                        if (dayNamePlacement == null)
                        {
                            errors.Add(new ValidationResult($"Поле {OutputTemplateConstants.DayName} отсутствует в определении шаблона", new List<string>() { "File" }));
                        }

                        //[время] [имяслужбы]
                        var timePlacements = weekDoc.MainDocumentPart.Document.Body
                            .FindElementsByText(OutputTemplateConstants.Time);

                        Text timePlacement = null;

                        if (timePlacements.Count() == 0)
                        {
                            errors.Add(new ValidationResult($"Поле {OutputTemplateConstants.Time} отсутствует в определении шаблона", new List<string>() { "File" }));
                        }
                        else if (timePlacements.Count() > 1)
                        {
                            errors.Add(new ValidationResult($"Поле {OutputTemplateConstants.Time} должно быть определено единожды в шаблоне", new List<string>() { "File" }));
                        }
                        else
                        {
                            timePlacement = timePlacements.First();
                        }

                        var worshipNamePlacements = weekDoc.MainDocumentPart.Document.Body
                            .FindElementsByText(OutputTemplateConstants.WorshipName);

                        Text worshipNamePlacement = null;

                        if (worshipNamePlacements.Count() == 0)
                        {
                            errors.Add(new ValidationResult($"Поле {OutputTemplateConstants.WorshipName} отсутствует в определении шаблона", new List<string>() { "File" }));
                        }
                        else if (worshipNamePlacements.Count() > 1)
                        {
                            errors.Add(new ValidationResult($"Поле {OutputTemplateConstants.WorshipName} должно быть определено единожды в шаблоне", new List<string>() { "File" }));
                        }
                        else
                        {
                            worshipNamePlacement = worshipNamePlacements.First();
                        }

                        if (timePlacement != null && worshipNamePlacement != null)
                        {
                            OpenXmlElement row = DocxUtility.FindCommonParent<TableRow>(timePlacement, worshipNamePlacement);

                            //может быть рядом в таблице
                            if (row == null)
                            {
                                row = DocxUtility.FindCommonParent<Paragraph>(timePlacement, worshipNamePlacement);
                            }

                            //или параграфом
                            if (row == null)
                            {
                                errors.Add(new ValidationResult($"Поля {OutputTemplateConstants.Time} и {OutputTemplateConstants.WorshipName} должны находиться либо внутри одной строки таблицы либо в одном параграфе документа"
                                    , new List<string>() { "File" }));
                            }
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
