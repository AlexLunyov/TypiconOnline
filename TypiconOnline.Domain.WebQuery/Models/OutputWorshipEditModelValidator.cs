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

            model.Name.ValidateRequired(nameof(model.Name), "Наименование", errors);

            model.AdditionalName.ValidateNotRequired(nameof(model.AdditionalName), "Наименование", errors);

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
