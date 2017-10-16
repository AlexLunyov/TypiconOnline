using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
