using System;
using System.Linq;
using JetBrains.Annotations;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Books
{
    /// <summary>
    /// Возвращает День Октоиха по заданной дате
    /// </summary>
    public class TheotokionAppQueryHandler : DbContextQueryBase, IQueryHandler<TheotokionAppQuery, YmnosGroup>
    {
        readonly ITypiconSerializer serializer;

        public TheotokionAppQueryHandler(TypiconDBContext dbContext, ITypiconSerializer serializer) : base(dbContext)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public YmnosGroup Handle([NotNull] TheotokionAppQuery query)
        {
            TheotokionApp theotokion = DbContext.Set<TheotokionApp>()
                                            .FirstOrDefault(c => c.Ihos == query.Ihos
                                                    && c.Place == query.Place
                                                    && c.DayOfWeek == query.DayOfWeek);

            YmnosGroup group = new YmnosGroup() { Ihos = theotokion.Ihos };
            group.Ymnis.Add(theotokion.GetElement(serializer));

            return group;
        }
    }
}
