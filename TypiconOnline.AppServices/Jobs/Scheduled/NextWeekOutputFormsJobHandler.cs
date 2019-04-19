using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Jobs.Scheduled
{
    /// <summary>
    /// Выполняет вычисление выходных форм для всех опубликованных Уставов
    /// на следующую неделю от текущего времени
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NextWeekOutputFormsJobHandler : ICommandHandler<NextWeekOutputFormsJob> 
    {
        private readonly TypiconDBContext _dbContext;
        private readonly IJobRepository _jobs;

        public NextWeekOutputFormsJobHandler(TypiconDBContext dbContext, IJobRepository jobs)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _jobs = jobs ?? throw new ArgumentNullException(nameof(jobs));
        }

        public Task<Result> ExecuteAsync(NextWeekOutputFormsJob job)
        {
            _jobs.Start(job);

            //находим дату с той недели, которую нужно вычислить
            var date = DateTime.Now.AddDays(7 * job.WeeksForward);

            foreach (var typ in _dbContext.GetAllPublishedVersions())
            {
                _jobs.Create(new CalculateOutputFormWeekJob(typ.TypiconId, typ.Id, date));
            }

            //завершаем задание для корректного логирования
            _jobs.Finish(job);
            //и запускаем ее снова для следующей даты
            _jobs.Create(job, job.NextDate);

            return Task.FromResult(Result.Ok());
        }
    }
}
