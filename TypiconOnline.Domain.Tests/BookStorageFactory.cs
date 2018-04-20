using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Repository.EF;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests
{
    public static class BookStorageFactory
    {
        public static BookStorage Create() => Create(UnitOfWorkFactory.Create());

        public static BookStorage Create(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            var easters = new EasterContext(unitOfWork);

            return new BookStorage(new EvangelionContext(unitOfWork),
                                    new ApostolContext(unitOfWork),
                                    new OldTestamentContext(unitOfWork),
                                    new PsalterContext(unitOfWork),
                                    new OktoikhContext(unitOfWork, easters),
                                    new TheotokionAppContext(unitOfWork),
                                    easters,
                                    new KatavasiaContext(unitOfWork));
        }
    }
}
