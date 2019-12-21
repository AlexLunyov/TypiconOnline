using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Web.Models.TypiconViewModels
{
    public class CreateTypiconModelValidator : IValidator<CreateTypiconModel>
    {
        const string DefaultLanguage = "cs-ru";

        public CreateTypiconModelValidator(TypiconDBContext dbContext) 
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected TypiconDBContext DbContext { get; }

        public IEnumerable<ValidationResult> Validate(CreateTypiconModel model)
        {
            var errors = new List<ValidationResult>();

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

            if (model.Description == null
                || !model.Description.Languages.Contains(DefaultLanguage))
            {
                errors.Add(new ValidationResult($"Описание должно быть определено на языке \"{DefaultLanguage}\"", new List<string>() { "Description" }));
            }
            if (model.Description?.IsValid == false)
            {
                errors.Add(new ValidationResult($"Описание заполнено с неверным определением языка", new List<string>() { "Description" }));
            }

            if (!string.IsNullOrEmpty(model.SystemName))
            {
                //проверяем на уникальность SystemName среди уже существующих TypiconEntity
                var found = DbContext.Set<TypiconEntity>()
                        .FirstOrDefault(c => c.SystemName == model.SystemName);

                if (found != null)
                {
                    errors.Add(new ValidationResult($"Устав с системным именем {model.SystemName} уже имеется", new List<string>() { "SystemName" }));
                }
            }

            return errors;
        }
    }
}
