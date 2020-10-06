using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TypiconOnline.Domain.WebQuery.Extensions;
using TypiconOnline.Domain.ItemTypes;
using System.Linq;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class OutputWorshipEditModelValidator : ModelValidatorBase<OutputWorshipEditModel>
    {
        public override IEnumerable<ValidationResult> Validate(OutputWorshipEditModel model)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(model.Name))
            {
                errors.Add(new ValidationResult($"Наименование обязательно для заполнения", new List<string>() { nameof(model.Name) }));
            }

            if (model.NameStyle == null)
            {
                errors.Add(new ValidationResult($"Стиль отображения службы не определен", new List<string>() { nameof(model.NameStyle) }));
            }

            if (!string.IsNullOrEmpty(model.AdditionalName)
                && model.AdditionalNameStyle == null)
            {
                errors.Add(new ValidationResult($"Стиль отображения дополнительного наименования службы не определен", new List<string>() { nameof(model.AdditionalNameStyle) }));
            }

            var time = new ItemTime(model.Time);

            if (!time.IsValid)
            {
                errors.AddRange(time.GetBrokenConstraints()
                    .Select(c => new ValidationResult($"{nameof(model.Time)} заполнено с неверным определением времени", new List<string>() { nameof(model.Time) })));
            }

            return errors;
        }
    }
}
