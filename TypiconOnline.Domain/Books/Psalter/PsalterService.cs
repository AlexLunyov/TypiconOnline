using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Psalter
{
    public class PsalterService : BookServiceBase, IPsalterService
    {
        public PsalterService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
