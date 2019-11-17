using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Interfaces;

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
            if (model.Name == null
                || !model.Name.Languages.Contains(DefaultLanguage))
            {
                errors.Add(new ValidationResult($"Наименование должно быть определено на языке \"{DefaultLanguage}\"", new List<string>() { "Name" }));
            }
            if (model.Name?.IsValid == false)
            {
                errors.Add(new ValidationResult($"Наименование заполнено с неверным определением языка", new List<string>() { "Name" }));
            }

            //ShortName
            if (model.ShortName?.IsEmpty == false)
            {
                if (!model.ShortName.Languages.Contains(DefaultLanguage))
                {
                    errors.Add(new ValidationResult($"Краткое наименование должно быть определено на языке \"{DefaultLanguage}\"", new List<string>() { "ShortName" }));
                }

                if (!model.ShortName.IsValid)
                {
                    errors.Add(new ValidationResult($"Краткое наименование заполнено с неверным определением языка", new List<string>() { "ShortName" }));
                }
            }

            return errors;
        }
    }
}
