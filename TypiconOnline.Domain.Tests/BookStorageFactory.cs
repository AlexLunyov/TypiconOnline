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

namespace TypiconOnline.Domain.Tests
{
    public static class BookStorageFactory
    {
        public static BookStorage Create() => Create(new EFUnitOfWork());

        public static BookStorage Create(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            return new BookStorage(new EvangelionService(unitOfWork),
                                    new ApostolService(unitOfWork),
                                    new OldTestamentService(unitOfWork),
                                    new PsalterService(unitOfWork),
                                    new OktoikhContext(unitOfWork),
                                    new TheotokionAppContext(unitOfWork),
                                    new EasterContext(unitOfWork));
        }
    }
}
