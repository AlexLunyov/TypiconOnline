using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Web.Services
{
    // Adapter that translates calls from IModelValidator into the IValidator<T>
    // application abstraction.
    class ModelValidator<TModel> : IModelValidator
    {
        private readonly IEnumerable<IValidator<TModel>> validators;

        public ModelValidator(IEnumerable<IValidator<TModel>> validators) =>
            this.validators = validators;

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext ctx) =>
            this.Validate((TModel)ctx.Model);

        private IEnumerable<ModelValidationResult> Validate(TModel model) =>
            from validator in this.validators
            from errorMessage in validator.Validate(model)
            from name in errorMessage.MemberNames
            select new ModelValidationResult(name, errorMessage.ErrorMessage);
    }
}
