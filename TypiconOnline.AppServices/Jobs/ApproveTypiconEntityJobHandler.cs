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
    /// Вычисляет выходные формы для каждого дня указанного года по версии Устава
    /// </summary>
    public class ApproveTypiconEntityJobHandler : JobHandlerBase<ApproveTypiconEntityJob>, ICommandHandler<ApproveTypiconEntityJob>
    {
        private readonly TypiconDBContext _dbContext;

        public ApproveTypiconEntityJobHandler(
            TypiconDBContext dbContext
            , IJobRepository jobs
            ) : base(jobs)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected override async Task<Result> DoTheJob(ApproveTypiconEntityJob job)
        {
            var typicon = _dbContext.GetTypiconEntity(job.TypiconId);

            if (typicon.Success)
            {
                if (!typicon.Value.TemplateId.HasValue)
                {
                    var err = $"Шаблон Устава не указан. Копирование Версии Устава не возможно.";

                    //SendMessage to Owner and sender

                    return Fail(job, err);
                }
                else if (typicon.Value.Status == TypiconStatus.WaitingApprovement)
                {
                    try
                    {
                        //TypiconEntity
                        typicon.Value.Status = TypiconStatus.Approving;
                        await _dbContext.UpdateTypiconEntityAsync(typicon.Value);

                        int templateId = typicon.Value.TemplateId.Value;

                        var version = _dbContext.GetPublishedVersion(templateId);

                        if (version.Success)
                        {
                            //TypiconVersion
                            var clone = version.Value.Clone();

                            //Ставим статус "черновик"
                            clone.BDate = null;
                            clone.VersionNumber = 1;
                            clone.TypiconId = job.TypiconId;

                            await _dbContext.UpdateTypiconVersionAsync(clone);

                            //TypiconEntity
                            typicon.Value.Status = TypiconStatus.Draft;
                            await _dbContext.UpdateTypiconEntityAsync(typicon.Value);

                            //SendMessage to Owner and sender
                            //nothing yet...

                            return Finish(job);
                        }
                        else
                        {
                            var err = $"Указанный Устав Id={templateId} либо не существует, либо не существует его опубликованная версия.";

                            //SendMessage to Owner and sender

                            return Fail(job, err);
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        return Fail(job, ex.Message);
                    }
                }
                else
                {
                    var err = $"Статус Устава Id={job.TypiconId} не находится в состоянии ожидания на утверждение.";

                    //SendMessage to Owner and sender

                    return Fail(job, err);
                }
            }
            else
            {
                //SendMessage to Owner and sender

                return Fail(job, typicon.Error);
            };
        }


        private TypiconVersion GetFullPublishedTypiconVersion(TypiconDBContext dbContext, int typiconId)
        {
            return dbContext.Set<TypiconVersion>()
                    .Include(c => c.Signs)
                        .ThenInclude(c => c.SignName)
                    .Include(c => c.CommonRules)
                    .Include(c => c.MenologyRules)
                        .ThenInclude(c => c.DayRuleWorships)
                    .Include(c => c.TriodionRules)
                        .ThenInclude(c => c.DayRuleWorships)
                    .Include(c => c.Kathismas)
                        .ThenInclude(c => c.SlavaElements)
                            .ThenInclude(c => c.PsalmLinks)
                    .Include(c => c.ExplicitAddRules)
                              .AsNoTracking()
                              .FirstOrDefault(c => c.TypiconId == typiconId && c.IsPublished);
        }

    }
}
