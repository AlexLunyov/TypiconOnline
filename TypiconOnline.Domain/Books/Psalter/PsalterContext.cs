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
        public PsalterContext(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public virtual GetPsalmResponse Get(GetPsalmRequest request)
        {
            var response = new GetPsalmResponse();

            try
            {
                response.Psalm = _unitOfWork.Repository<Psalm>()
                                            .Get(c => c.Number == request.Number);

                //response.BookElement = response.Psalm.GetElement();
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }
    }
}
