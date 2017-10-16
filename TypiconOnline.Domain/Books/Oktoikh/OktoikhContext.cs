using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Oktoikh
{
    public class OktoikhContext : BookServiceBase, IOktoikhContext
    {
        public virtual List<OktoikhGlas> OktoikhGlasSet { get; set; }

        public OktoikhContext(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            OktoikhGlasSet = new List<OktoikhGlas>();
        }

        /// <summary>
        /// Возвращает текст богослужения дня и гласа, соответствующих заданной дате
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public OktoikhDay Get(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
