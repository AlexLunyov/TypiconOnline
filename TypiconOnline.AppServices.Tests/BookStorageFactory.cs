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
using TypiconOnline.Domain.Books.Katavasia;

namespace TypiconOnline.AppServices.Tests
{
    public static class BookStorageFactory
    {
        public static BookStorage Create()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            var easters = new EasterContext(_unitOfWork);

            return new BookStorage(new EvangelionContext(_unitOfWork),
                                    new ApostolContext(_unitOfWork),
                                    new OldTestamentContext(_unitOfWork),
                                    new PsalterContext(_unitOfWork),
                                    new OktoikhContext(_unitOfWork, easters),
                                    new TheotokionAppContext(_unitOfWork),
                                    new EasterContext(_unitOfWork),
                                    new KatavasiaContext(_unitOfWork));
        }
    }
}
