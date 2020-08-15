using System;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Jobs
{
    /// <summary>
    /// Вычисляет выходные формы для недели по версии Устава
    /// </summary>
    public class CalculateOutputFormWeekJobHandler : ICommandHandler<CalculateOutputFormWeekJob>
    {
        private const int DELAY = 5000; 

        private readonly TypiconDBContext _dbContext;
        private readonly IOutputDayFactory _outputFormFactory;
        private readonly IJobRepository _jobs;
        //private readonly ICommandProcessor _commandProcessor;

        public CalculateOutputFormWeekJobHandler(TypiconDBContext dbContext
            , IOutputDayFactory outputFormFactory
            , IJobRepository jobs
            /*, ICommandProcessor commandProcessor*/)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _outputFormFactory = outputFormFactory ?? throw new ArgumentNullException(nameof(outputFormFactory));
            _jobs = jobs ?? throw new ArgumentNullException(nameof(jobs));
            //_commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
        }

        public Task<Result> ExecuteAsync(CalculateOutputFormWeekJob job)
        {
            _jobs.Start(job);

            //Проверяем наличие ModifiedYear Для начала и конца недели
            var start = CheckModifiedYearsExistance(job.TypiconVersionId, job.Date.Year);
            var finish = CheckModifiedYearsExistance(job.TypiconVersionId, job.Date.AddDays(7).Year);

            Result.Combine(start, finish)
                .OnSuccess(() =>
                {
                    //все найдено - вычисляем выходные формы
                    DoTheJob(job);
                    _jobs.Finish(job);
                })
                .OnFailure(() =>
                {
                    //перезапускаем задачу
                    _jobs.Recreate(job, DELAY);
                });

            return Task.FromResult(Result.Ok());
        }

        private Result CheckModifiedYearsExistance(int id, int year)
        {
            if (!_dbContext.IsModifiedYearExists(id, year))
            {
                _jobs.Create(new CalculateModifiedYearJob(id, year));

                return Result.Fail("");
            }
            return Result.Ok();
        }

        private Task DoTheJob(CalculateOutputFormWeekJob job)
        {
            var week = _outputFormFactory.CreateWeek(new CreateOutputWeekRequest()
            {
                TypiconId = job.TypiconId,
                TypiconVersionId = job.TypiconVersionId,
                Date = job.Date,
                //вычисляем только несуществующие дни
                RecalculateAllDays = false
            });

            foreach (var day in week)
            {
                _dbContext.UpdateOutputDay(day);
            }

            return Task.CompletedTask;
        }


    }
}
