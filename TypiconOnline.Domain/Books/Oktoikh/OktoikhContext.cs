using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Oktoikh
{
    /// <summary>
    /// Реализация IOktoikhContext, дающая доступ к богослужебным текстам Октоиха
    /// </summary>
    public class OktoikhContext : BookServiceBase, IOktoikhContext
    {
        public OktoikhContext(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Возвращает текст богослужения дня и гласа, соответствующих заданной дате
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public OktoikhDay Get(DateTime date)
        {
            int ihos = OktoikhCalculator.CalculateIhosNumber(date);

            return _unitOfWork.Repository<OktoikhDay>().Get(c => c.Ihos == ihos && c.DayOfWeek == date.DayOfWeek);
        }
    }
}
