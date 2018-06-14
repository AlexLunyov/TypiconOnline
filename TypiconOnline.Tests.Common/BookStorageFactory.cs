using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Tests.Common
{
    public class BookStorageFactory
    {
        public static BookStorage Create()
        {
            return Create(UnitOfWorkFactory.Create());
        }

        public static BookStorage Create(IUnitOfWork unitOfWork)
        {
            var easters = new EasterContext(unitOfWork);

            return new BookStorage(new EvangelionContext(unitOfWork),
                                    new ApostolContext(unitOfWork),
                                    new OldTestamentContext(unitOfWork),
                                    new PsalterContext(unitOfWork),
                                    new OktoikhContext(unitOfWork, easters),
                                    new TheotokionAppContext(unitOfWork),
                                    easters,
                                    new KatavasiaContext(unitOfWork),
                                    new WeekDayAppContext(unitOfWork));
        }
    }
}
