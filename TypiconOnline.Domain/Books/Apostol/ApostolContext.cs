using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Apostol
{
    public class ApostolContext : BookServiceBase, IApostolContext
    {
        public ApostolContext(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
