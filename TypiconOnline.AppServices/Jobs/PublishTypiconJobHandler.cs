using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Extensions;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Jobs
{
    /// <summary>
    /// Публикует черновик Указанного Устава
    /// </summary>
    public class PublishTypiconJobHandler : JobHandlerBase<PublishTypiconJob>, ICommandHandler<PublishTypiconJob>
    {
        private readonly TypiconDBContext _dbContext;

        public PublishTypiconJobHandler(
            TypiconDBContext dbContext
            , IJobRepository jobs
            ) : base(jobs)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected override async Task<Result> DoTheJob(PublishTypiconJob job)
        {
            //находим черновик
            var found = _dbContext.GetTypiconVersion(job.TypiconId, TypiconVersionStatus.Draft);

            if (found.Success)
            {
                var version = found.Value;
                if (version.Typicon.Status == TypiconStatus.Approving
                    || version.Typicon.Status == TypiconStatus.Publishing
                    || version.Typicon.Status == TypiconStatus.Validating)
                {
                    return Fail(job, "Устав находится в состоянии, не подлежащем для публикации");
                }
                //Шаблон и есть переменные - так нельзя публиковать
                else if (!version.IsTemplate && version.TypiconVariables.Any())
                {
                    return Fail(job, "Устав находится в состоянии, не подлежащем для публикации. Устав должен быть либо определен как Шаблон, либо всем Переменным должны быть заданы значения.");
                }
                else
                {
                    var prevStatus = version.Typicon.Status;
                    //TypiconEntity
                    version.Typicon.Status = TypiconStatus.Publishing;
                    await _dbContext.UpdateTypiconEntityAsync(version.Typicon);

                    using (var transaction = _dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            /*
                             * Проверяем валидность...
                             */
                            //version.Value.ValidationStatus

                            version.IsModified = false;
                            version.ModifiedYears.Clear();

                            //new draft
                            var clone = version.Clone();
                            clone.TypiconId = job.TypiconId;
                            clone.VersionNumber = version.VersionNumber + 1;
                            clone.BDate = null;
                            await _dbContext.UpdateTypiconVersionAsync(clone);

                            //old publish
                            var oldPublish = _dbContext.GetTypiconVersion(job.TypiconId, TypiconVersionStatus.Published);
                            if (oldPublish.Success)
                            {
                                oldPublish.Value.EDate = DateTime.Now;
                            }

                            //new publish
                            version.BDate = DateTime.Now;

                            //outputforms
                            await _dbContext.ClearOutputFormsAsync(job.TypiconId);

                            //TODO: SendMessage to Owner and sender


                            //typiconEntity
                            version.Typicon.Status = TypiconStatus.Published;

                            _dbContext.SaveChanges();

                            transaction.Commit();

                            return Finish(job);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();

                            version.Typicon.Status = prevStatus;
                            version.IsModified = true;
                            await _dbContext.UpdateTypiconEntityAsync(version.Typicon);

                            return Fail(job, ex.Message);
                        }
                    }
                }
            }
            else
            {
                //TODO: SendMessage to Owner and sender

                return Fail(job, found.Error);
            };
        }

    }
}
