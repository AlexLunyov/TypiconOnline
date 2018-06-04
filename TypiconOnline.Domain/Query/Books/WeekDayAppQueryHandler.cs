using JetBrains.Annotations;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Books
{
    /// <summary>
    /// Возвращает День Октоиха по заданной дате
    /// </summary>
    public class WeekDayAppQueryHandler : UnitOfWorkHandlerBase, IDataQueryHandler<WeekDayAppQuery, WeekDayApp>
    {
        public WeekDayAppQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public WeekDayApp Handle([NotNull] WeekDayAppQuery query)
        {
            return UnitOfWork.Repository<WeekDayApp>().Get(c => c.DayOfWeek == query.DayOfWeek);
        }
    }
}
