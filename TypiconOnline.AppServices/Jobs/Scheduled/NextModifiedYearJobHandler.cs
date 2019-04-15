using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Jobs.Scheduled
{
    /// <summary>
    /// Выполняет вычисление выходных форм для всех опубликованных Уставов
    /// на следующую неделю от текущего времени
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NextModifiedYearJobHandler : ICommandHandler<NextModifiedYearJob> 
    {
        private readonly TypiconDBContext _dbContext;
        private readonly IJobRepository _jobs;

        public NextModifiedYearJobHandler(TypiconDBContext dbContext, IJobRepository jobs)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _jobs = jobs ?? throw new ArgumentNullException(nameof(jobs));
        }

        public Task ExecuteAsync(NextModifiedYearJob job)
        {
            _jobs.Start(job);

            int i = job.YearsCount;
            var year = DateTime.Now.Year;

            while (i > 0)
            {
                //Добавляем год
                year++;

                foreach (var typ in _dbContext.GetAllPublishedVersions())
                {
                    _jobs.Create(new CalculateModifiedYearJob(typ.Id, year));
                }

                i--;
            }

            //завершаем задание для корректного логирования
            _jobs.Finish(job);
            //и запускаем ее снова для следующей даты
            _jobs.Create(job, job.NextDate);

            return Task.CompletedTask;
        }

    }
}
