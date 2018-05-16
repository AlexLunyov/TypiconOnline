using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.WeekDayApp;

namespace TypiconOnline.Domain.Books
{
    public class BookStorageFactory : IBookStorageFactory
    {
        private IUnitOfWork _unitOfWork;

        public BookStorageFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
        }

        public BookStorage Create()
        {
            var easters = new EasterContext(_unitOfWork);

            return new BookStorage(new EvangelionContext(_unitOfWork),
                                    new ApostolContext(_unitOfWork),
                                    new OldTestamentContext(_unitOfWork),
                                    new PsalterContext(_unitOfWork),
                                    new OktoikhContext(_unitOfWork, easters),
                                    new TheotokionAppContext(_unitOfWork),
                                    new EasterContext(_unitOfWork),
                                    new KatavasiaContext(_unitOfWork),
                                    new WeekDayAppContext(_unitOfWork));
        }

        public static BookStorage Create(IUnitOfWork unitOfWork)
        {
            return new BookStorageFactory(unitOfWork).Create();
        }
    }
}
