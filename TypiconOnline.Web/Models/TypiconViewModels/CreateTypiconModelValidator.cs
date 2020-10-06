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
            if (string.IsNullOrEmpty(model.Name))
            {
                errors.Add(new ValidationResult($"Наименование обязательно для заполнения", new List<string>() { nameof(model.Name) }));
            }

            //Description
            if (string.IsNullOrEmpty(model.Description))
            {
                errors.Add(new ValidationResult($"Описание обязательно для заполнения", new List<string>() { nameof(model.Description) }));
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
