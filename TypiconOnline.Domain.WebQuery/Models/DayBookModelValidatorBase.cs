using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.WebQuery.Extensions;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class DayBookModelValidatorBase<T> : BookModelValidatorBase<T> where T : class, new()
    {
        const string DefaultLanguage = "cs-ru";

        public DayBookModelValidatorBase(ITypiconSerializer serializer) : base(serializer) { }

        protected List<ValidationResult> ValidateDay(DayBookModelBase model)
        {
            var errors = ValidateBook(model);

            //Name
            //model.Name.ValidateRequired(nameof(model.Name), "Наименование", errors);
            if (string.IsNullOrEmpty(model.Name))
            {
                errors.Add(new ValidationResult($"Наименование обязательно для заполнения", new List<string>() { nameof(model.Name) }));
            }

            //ShortName
            //model.ShortName.ValidateNotRequired(nameof(model.Name), "Краткое наименование", errors);

            return errors;
        }
    }
}
