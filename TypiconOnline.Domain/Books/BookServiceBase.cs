using System;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books
{
    public abstract class BookServiceBase
    {
        protected IUnitOfWork _unitOfWork;

        public BookServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
        }
    }
}
