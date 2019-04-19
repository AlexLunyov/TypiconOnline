using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Jobs.Validation
{
    /// <summary>
    /// Проводит валидацию MenologyRule
    /// </summary>
    public class ValidateTypiconVersionJobHandler : ValidateJobHandlerBase, ICommandHandler<ValidateTypiconVersionJob>
    {
        private const int DELAY = 15000;

        public ValidateTypiconVersionJobHandler(TypiconDBContext dbContext, IJobRepository jobs, IRuleSerializerRoot serializer)
            : base(dbContext, jobs, serializer)
        {
        }

        public Task<Result> ExecuteAsync(ValidateTypiconVersionJob job)
        {
            Jobs.Start(job);

            var version = DbContext.GetTypiconVersion(job.Id);

            var result = Result.Ok();

            version.OnSuccess(async () =>
            {
                await DoTheJob(job, version.Value);
            })
            .OnFailure(() =>
            {
                var err = $"Версия Устава с Id = {job.Id} для валидации не найдена.";
                Jobs.Fail(job, err);

                result = Result.Fail(err);
            });

            return Task.FromResult(result);
        }

        private async Task DoTheJob(ValidateTypiconVersionJob job, TypiconVersion version)
        {
            if (version.ValidationStatus == ValidationStatus.InProcess)
            {
                //перезапускаем задачу
                Jobs.Recreate(job, DELAY);
            }
            else
            {
                await DbContext.ClearTypiconVersionErrorsAsync(version.Id);

                var errs = version.GetBrokenConstraints(Serializer);

                if (errs.Count > 0)
                {
                    version.ValidationStatus = ValidationStatus.Invalid;
                    await DbContext.AddTypiconVersionErrorAsync(errs);
                }
                else
                {
                    version.ValidationStatus = ValidationStatus.Invalid;
                }

                await DbContext.UpdateTypiconVersionAsync(version);

                Jobs.Finish(job);
            }
        }
    }
}
