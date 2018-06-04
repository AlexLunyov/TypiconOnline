using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.OldTestament
{
    public class OldTestamentContext : BookServiceBase, IOldTestamentContext
    {
        public OldTestamentContext(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
