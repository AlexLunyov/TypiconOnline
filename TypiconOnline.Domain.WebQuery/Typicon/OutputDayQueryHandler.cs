using JetBrains.Annotations;
using Mapster;
using System;
using System.Linq;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Output;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает фильтрованную Выходную форму для недели
    /// </summary>
    public class OutputDayQueryHandler : QueryStrategyHandlerBase, IQueryHandler<OutputDayQuery, Result<FilteredOutputDay>>
    {
        private readonly IJobRepository _jobs;

        public OutputDayQueryHandler(TypiconDBContext dbContext
            , IQueryProcessor queryProcessor
            , IJobRepository jobs)
            : base(dbContext, queryProcessor)
        {
            _jobs = jobs ?? throw new ArgumentNullException(nameof(jobs));
        }

        public Result<FilteredOutputDay> Handle([NotNull] OutputDayQuery query)
        {
            var date = query.Date.Date;

            var scheduleDay = DbContext.Set<OutputDay>().FirstOrDefault(c => c.TypiconId == query.TypiconId && c.Date == date);

            //нашли сформированный день
            if (scheduleDay != null)
            {
                return Result.Ok(scheduleDay.FilterOut(query.Filter));
            }

            //добавляем задачу на формирование дня
            var version = QueryProcessor.Process(new TypiconPublishedVersionQuery(query.TypiconId));

            if (version.Failure)
            {
                return Result.Fail<FilteredOutputDay>(version.Error);
            }
            else
            {
                //добавляем задание на формирование выходных форм
                _jobs.Create(new CalculateOutputFormWeekJob(query.TypiconId, version.Value.Id, date));

                return Result.Fail<FilteredOutputDay>($"Инициировано формирование расписания. Повторите операцию позже.");
            }
        }
    }
}
