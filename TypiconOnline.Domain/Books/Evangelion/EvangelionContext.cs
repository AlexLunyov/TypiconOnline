using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Evangelion
{
    public class EvangelionContext : BookServiceBase, IEvangelionContext
    {
        public EvangelionContext(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
