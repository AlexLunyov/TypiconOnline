using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Psalter
{
    public class PsalterContext : BookServiceBase, IPsalterContext
    {
        public PsalterContext(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
