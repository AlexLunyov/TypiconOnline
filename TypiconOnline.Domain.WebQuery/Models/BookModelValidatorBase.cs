using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class BookModelValidatorBase<T> : ModelValidatorBase<T> where T: class, new()
    {
        public BookModelValidatorBase(ITypiconSerializer serializer)
        {
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        protected ITypiconSerializer Serializer { get; }

        protected List<ValidationResult> ValidateBook(BookModelBase model)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(model.Definition))
            {
                errors.Add(new ValidationResult("Отсутствует определение Текста службы", new List<string>() { "Definition" }));
            }
            else
            {
                //TODO: избавиться от зависимости от XmlException
                try
                {
                    var text = Serializer.Deserialize<DayContainer>(model.Definition);
                    if (text == null)
                    {
                        errors.Add(new ValidationResult("Определение Текста службы заполнено с неопределяемыми системой ошибками", new List<string>() { "Definition" }));
                    }
                    else if (!text.IsValid)
                    {
                        errors.Add(new ValidationResult(text.GetBrokenConstraints().GetSummary(), new List<string>() { "Definition" }));
                    }
                }
                catch (Exception ex) when (ex is XmlException || ex is InvalidOperationException)
                {
                    errors.Add(new ValidationResult($"Определение Текста службы заполнено с ошибкой: {ex.Message}", new List<string>() { "Definition" }));
                }
                
            }

            return errors;
        }
    }
}
