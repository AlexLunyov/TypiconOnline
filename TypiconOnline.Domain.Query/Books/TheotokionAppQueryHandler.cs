using JetBrains.Annotations;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Books
{
    /// <summary>
    /// Возвращает День Октоиха по заданной дате
    /// </summary>
    public class TheotokionAppQueryHandler : UnitOfWorkHandlerBase, IDataQueryHandler<TheotokionAppQuery, YmnosGroup>
    {
        public TheotokionAppQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public YmnosGroup Handle([NotNull] TheotokionAppQuery query)
        {
            TheotokionApp theotokion = UnitOfWork.Repository<TheotokionApp>()
                                            .Get(c => c.Ihos == query.Ihos
                                                    && c.Place == query.Place
                                                    && c.DayOfWeek == query.DayOfWeek);

            YmnosGroup group = new YmnosGroup() { Ihos = theotokion.Ihos };
            group.Ymnis.Add(theotokion.GetElement());

            return group;
        }
    }
}
