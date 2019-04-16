using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Jobs.Validation
{
    /// <summary>
    /// Вычисляет выходные формы для каждого дня указанного года по версии Устава
    /// </summary>
    public abstract class ValidateRuleJobHandler<T>: ValidateJobHandlerBase where T: RuleEntity, new()
    {
        private const int DELAY = 5000; 

        public ValidateRuleJobHandler(TypiconDBContext dbContext, IJobRepository jobs, IRuleSerializerRoot serializer)
            : base(dbContext, jobs, serializer)
        {
        }

        protected async Task Handle(ValidateRuleJob<T> job, string entityName)
        {
            Jobs.Start(job);

            var rule = DbContext.GetRule<T>(job.Id);

            if (rule != null)
            {
                if (rule.TypiconVersion.ValidationStatus == ValidationStatus.InProcess)
                {
                    //перезапускаем задачу
                    Jobs.Recreate(job, DELAY);
                }
                else
                {
                    rule.TypiconVersion.ValidationStatus = ValidationStatus.InProcess;

                    await DbContext.UpdateTypiconVersionAsync(rule.TypiconVersion);

                    await DbContext.ClearRuleErrorsAsync(rule.Id);

                    var errs = rule.GetBrokenConstraints(Serializer);

                    if (errs.Count > 0)
                    {
                        rule.TypiconVersion.ValidationStatus = ValidationStatus.Invalid;
                        await DbContext.AddTypiconVersionErrorAsync(new TypiconVersionError(rule.TypiconVersionId, errs.GetSummary(), entityName, rule.Id));
                    }
                    else
                    {
                        //смотрим, если ошибок нет в бд, то назначаем TypiconVersion.validationStatus = Valid
                        var versionErrors = DbContext.GetErrorsFromDb(rule.TypiconVersionId);

                        rule.TypiconVersion.ValidationStatus = (versionErrors.Count() == 0) ? ValidationStatus.Valid : ValidationStatus.Invalid;
                    }

                    await DbContext.UpdateTypiconVersionAsync(rule.TypiconVersion);

                    Jobs.Finish(job);
                }
            }
            else
            {
                Jobs.Fail(job, $"Правило {typeof(T).Name} с Id = {job.Id} для валидации не найдено.");
            }
        }
    }
}
