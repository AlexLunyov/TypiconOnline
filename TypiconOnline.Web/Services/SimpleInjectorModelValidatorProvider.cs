using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SimpleInjector;

namespace TypiconOnline.Web.Services
{
    public class SimpleInjectorModelValidatorProvider : IModelValidatorProvider
    {
        private readonly Container container;

        public SimpleInjectorModelValidatorProvider(Container container) =>
            this.container = container;

        public void CreateValidators(ModelValidatorProviderContext ctx)
        {
            var validatorType =
                typeof(ModelValidator<>).MakeGenericType(ctx.ModelMetadata.ModelType);
            var validator = (IModelValidator)this.container.GetInstance(validatorType);
            ctx.Results.Add(new ValidatorItem { Validator = validator });
        }
    }
}
