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
            var version = _dbContext.GetTypiconVersion(job.TypiconId, TypiconVersionStatus.Draft);

            if (version.Success)
            {
                if (version.Value.Typicon.Status == TypiconStatus.Approving
                    || version.Value.Typicon.Status == TypiconStatus.Publishing
                    || version.Value.Typicon.Status == TypiconStatus.Validating)
                {
                    return Fail(job, "Устав находится в состоянии, не подлежащем для публикации");
                }
                else
                {
                    var prevStatus = version.Value.Typicon.Status;
                    //TypiconEntity
                    version.Value.Typicon.Status = TypiconStatus.Publishing;
                    await _dbContext.UpdateTypiconEntityAsync(version.Value.Typicon);

                    using (var transaction = _dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            /*
                             * Проверяем валидность...
                             */
                            //version.Value.ValidationStatus

                            version.Value.IsModified = false;
                            version.Value.ModifiedYears.Clear();

                            //new draft
                            var clone = version.Value.Clone();
                            clone.TypiconId = job.TypiconId;
                            clone.VersionNumber = version.Value.VersionNumber + 1;
                            clone.BDate = null;
                            await _dbContext.UpdateTypiconVersionAsync(clone);

                            //old publish
                            var oldPublish = _dbContext.GetTypiconVersion(job.TypiconId, TypiconVersionStatus.Published);
                            if (oldPublish.Success)
                            {
                                oldPublish.Value.EDate = DateTime.Now;
                            }

                            //new publish
                            version.Value.BDate = DateTime.Now;

                            //outputforms
                            await _dbContext.ClearOutputFormsAsync(job.TypiconId);

                            //SendMessage to Owner and sender


                            //typiconEntity
                            version.Value.Typicon.Status = TypiconStatus.Published;

                            _dbContext.SaveChanges();

                            transaction.Commit();

                            return Finish(job);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();

                            version.Value.Typicon.Status = prevStatus;
                            await _dbContext.UpdateTypiconEntityAsync(version.Value.Typicon);

                            return Fail(job, ex.Message);
                        }
                    }
                }
            }
            else
            {
                //SendMessage to Owner and sender

                return Fail(job, version.Error);
            };
        }

    }
}
