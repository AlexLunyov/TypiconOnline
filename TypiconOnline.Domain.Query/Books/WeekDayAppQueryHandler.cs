using JetBrains.Annotations;
using System.Linq;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Books
{
    /// <summary>
    /// Возвращает Приложение на каждый день недели
    /// </summary>
    public class WeekDayAppQueryHandler : DbContextHandlerBase, IDataQueryHandler<WeekDayAppQuery, WeekDayApp>
    {
        public WeekDayAppQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public WeekDayApp Handle([NotNull] WeekDayAppQuery query)
        {
            return DbContext.Set<WeekDayApp>().FirstOrDefault(c => c.DayOfWeek == query.DayOfWeek);
        }
    }
}
