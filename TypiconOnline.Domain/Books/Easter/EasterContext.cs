using System;
using System.Collections.Generic;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Easter
{
    public class EasterContext : BookServiceBase, IEasterContext
    {
        public EasterContext(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public IEnumerable<EasterItem> GetAll()
        {
            return _unitOfWork.Repository<EasterItem>().GetAll();
        }

        public DateTime GetCurrentEaster(int year)
        {
            EasterItem easter = _unitOfWork.Repository<EasterItem>().Get(c => c.Date.Year == year);
            if (easter == null)
                throw new NullReferenceException("День празднования Пасхи не определен для года " + year);

            return easter.Date;
        }

        public int GetDaysFromCurrentEaster(DateTime date)
        {
            DateTime easterDate = GetCurrentEaster(date.Year);

            //вычитаем из даты, потому как неверно считает, если время оставить
            return date.Date.Subtract(easterDate.Date).Days;
        }
    }
}
