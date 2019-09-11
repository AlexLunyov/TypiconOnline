using JetBrains.Annotations;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает фильтрованную Выходную форму для недели
    /// </summary>
    public class OutputWeekQueryHandler : QueryStrategyHandlerBase, IQueryHandler<OutputWeekQuery, Result<FilteredOutputWeek>>
    {
        public OutputWeekQueryHandler(TypiconDBContext dbContext, IQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor) { }

        public Result<FilteredOutputWeek> Handle([NotNull] OutputWeekQuery query)
        {
            var date = EachDayPerWeek.GetMonday(query.Date);

            var week = new FilteredOutputWeek()
            {
                Name = QueryProcessor.Process(new WeekNameQuery(query.TypiconId, date, false, query.Filter.Language))
            };

            int i = 0;

            while (i < 7) 
            {
                var dayResult = QueryProcessor.Process(new OutputDayQuery(query.TypiconId, date, query.Filter));

                if (dayResult.Failure)
                {
                    return Result.Fail<FilteredOutputWeek>(dayResult.Error);
                }

                week.Days.Add(dayResult.Value);
                date = date.AddDays(1);
                i++;
            }

            return Result.Ok(week);
        }
    }
}
