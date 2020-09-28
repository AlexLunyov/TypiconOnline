using JetBrains.Annotations;
using System.Linq;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает фильтрованную Выходную форму для недели
    /// </summary>
    public class OutputWeekBySystemNameQueryHandler : QueryStrategyHandlerBase, IQueryHandler<OutputWeekBySystemNameQuery, Result<(int, FilteredOutputWeek)>>
    {
        public OutputWeekBySystemNameQueryHandler(TypiconDBContext dbContext, IQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor) { }

        public Result<(int, FilteredOutputWeek)> Handle([NotNull] OutputWeekBySystemNameQuery query)
        {
            var date = EachDayPerWeek.GetMonday(query.Date);

            var typicon = DbContext.Set<TypiconEntity>().FirstOrDefault(c => c.SystemName.ToLower() == query.SystemName.ToLower());

            if (typicon == null)
            {
                return Result.Fail<(int, FilteredOutputWeek)>($"Ошибка: Устав с системным именем {query.SystemName} не был найден.");
            }

            var week = new FilteredOutputWeek()
            {
                Name = QueryProcessor.Process(new WeekNameQuery(typicon.Id, date, false, query.Filter.Language))
            };

            int i = 0;

            while (i < 7) 
            {
                var dayResult = QueryProcessor.Process(new OutputDayQuery(typicon.Id, date, query.Filter));

                if (dayResult.Failure)
                {
                    return Result.Fail<(int, FilteredOutputWeek)>(dayResult.Error);
                }

                //доблавяем только день, у которого есть богослужения
                if (dayResult.Value.Worships.Count > 0)
                {
                    week.Days.Add(dayResult.Value);
                }
                
                date = date.AddDays(1);
                i++;
            }

            return Result.Ok((typicon.Id, week));
        }
    }
}
