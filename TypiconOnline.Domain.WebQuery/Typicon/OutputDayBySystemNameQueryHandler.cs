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
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает фильтрованную Выходную форму для дня
    /// </summary>
    public class OutputDayBySystemNameQueryHandler : QueryStrategyHandlerBase, IQueryHandler<OutputDayBySystemNameQuery, Result<FilteredOutputDay>>
    {
        private readonly IJobRepository _jobs;

        public OutputDayBySystemNameQueryHandler(TypiconDBContext dbContext
            , IQueryProcessor queryProcessor
            , IJobRepository jobs)
            : base(dbContext, queryProcessor)
        {
            _jobs = jobs ?? throw new ArgumentNullException(nameof(jobs));
        }

        public Result<FilteredOutputDay> Handle([NotNull] OutputDayBySystemNameQuery query)
        {
            var typicon = DbContext.Set<TypiconEntity>().FirstOrDefault(c => c.SystemName.ToLower() == query.SystemName.ToLower());

            if (typicon == null)
            {
                return Result.Fail<FilteredOutputDay>($"Ошибка: Устав с системным именем {query.SystemName} не был найден.");
            }

            var date = query.Date.Date;

            var scheduleDay = DbContext.Set<OutputDay>().FirstOrDefault(c => c.TypiconId == typicon.Id && c.Date == date);

            //нашли сформированный день
            if (scheduleDay != null)
            {
                return Result.Ok(scheduleDay.FilterOut(query.Filter));
            }

            //добавляем задачу на формирование дня
            var version = QueryProcessor.Process(new TypiconPublishedVersionQuery(typicon.Id));

            if (version.Failure)
            {
                return Result.Fail<FilteredOutputDay>(version.Error);
            }
            else
            {
                //добавляем задание на формирование выходных форм
                _jobs.Create(new CalculateOutputFormWeekJob(typicon.Id, version.Value.Id, date));

                return Result.Fail<FilteredOutputDay>($"Инициировано формирование расписания. Повторите операцию позже.");
            }
        }
    }
}
