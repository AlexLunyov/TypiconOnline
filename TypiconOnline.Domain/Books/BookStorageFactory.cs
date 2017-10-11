using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Apostol;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Evangelion;
using TypiconOnline.Domain.Books.Irmologion;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.OldTestament;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books
{
    public class BookStorageFactory : IBookStorageFactory
    {
        private IUnitOfWork _unitOfWork;

        public BookStorageFactory(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        public BookStorage Create()
        {
            return new BookStorage(new EvangelionService(_unitOfWork),
                                    new ApostolService(_unitOfWork),
                                    new OldTestamentService(_unitOfWork),
                                    new PsalterService(_unitOfWork),
                                    new OktoikhService(_unitOfWork),
                                    new ReadOnlyIrmTheotokionService(_unitOfWork),
                                    new EasterService(_unitOfWork));
        }
    }
}
