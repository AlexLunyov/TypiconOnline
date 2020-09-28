using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Extensions;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Jobs
{
    /// <summary>
    /// Вычисляет выходные формы для каждого дня указанного года по версии Устава
    /// </summary>
    public class ApproveTypiconClaimJobHandler : JobHandlerBase<ApproveTypiconClaimJob>, ICommandHandler<ApproveTypiconClaimJob>
    {
        private readonly TypiconDBContext _dbContext;

        public ApproveTypiconClaimJobHandler(
            TypiconDBContext dbContext
            , IJobRepository jobs
            ) : base(jobs)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected override async Task<Result> DoTheJob(ApproveTypiconClaimJob job)
        {
            var r = _dbContext.GetTypiconClaim(job.TypiconId);

            if (r.Value is TypiconClaim claim)
            {
                if (!claim.TemplateId.HasValue)
                {
                    var err = $"Шаблон Устава не указан. Копирование Версии Устава не возможно.";

                    //SendMessage to Owner and sender

                    return Fail(job, err);
                }
                else if (claim.Status == TypiconClaimStatus.WatingForReview)
                {
                    try
                    {
                        //TypiconClaim
                        claim.Status = TypiconClaimStatus.InProcess;

                        await _dbContext.UpdateTypiconClaimAsync(claim);

                        int templateId = claim.TemplateId.Value;

                        var version = _dbContext.GetPublishedVersion(templateId);

                        if (version.Success)
                        {
                            //TypiconVersion
                            var clone = version.Value.Clone();

                            clone.Name = new ItemText(claim.Name);
                            clone.Description = new ItemText(claim.Description);

                            //Ставим статус "черновик"
                            clone.BDate = null;
                            clone.VersionNumber = 1;
                            clone.TypiconId = job.TypiconId;
                            //ставим true для возможности сразу опубликовать Устав
                            clone.IsModified = true;

                            //new TypiconEntity
                            var entity = new TypiconEntity()
                            {
                                SystemName = claim.SystemName,
                                DefaultLanguage = claim.DefaultLanguage,
                                OwnerId = claim.OwnerId,
                                Status = TypiconStatus.Approving,
                                TemplateId = claim.TemplateId
                            };
                            entity.Versions.Add(clone);
                            //добавляем Устав и его первую пустую версию
                            await _dbContext.AddTypiconEntityAsync(entity);

                            //добавляем вложенные коллекции в версию
                            version.Value.CopyChildrenTo(clone);
                            await _dbContext.UpdateTypiconVersionAsync(clone);

                            //TypiconEntity
                            entity.Status = TypiconStatus.Draft;
                            await _dbContext.UpdateTypiconEntityAsync(entity);

                            //remove claim
                            await _dbContext.RemoveTypiconClaimAsync(claim);

                            //SendMessage to Owner and sender
                            //nothing yet...

                            return Finish(job);
                        }
                        else
                        {
                            var err = $"Указанный Устав Id={templateId} либо не существует, либо не существует его опубликованная версия.";

                            //SendMessage to Owner and sender

                            await FailClaimAsync(claim, err);

                            return Fail(job, err);
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        await FailClaimAsync(claim, "при сохранении в БД");

                        return Fail(job, ex.Message);
                    }
                }
                else
                {
                    var err = $"Статус Устава Id={job.TypiconId} не находится в состоянии ожидания на утверждение.";

                    await FailClaimAsync(claim, err);

                    //SendMessage to Owner and sender

                    return Fail(job, err);
                }
            }
            else
            {
                //SendMessage to Owner and sender

                return Fail(job, r.Error);
            };
        }

        private async Task FailClaimAsync(TypiconClaim claim, string msg)
        {
            claim.Status = TypiconClaimStatus.WatingForReview;
            claim.ResultMesasge = $"Произошла ошибка: {msg}";

            await _dbContext.UpdateTypiconClaimAsync(claim);
        }
    }
}
