using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Irmologion
{
    public class IrmologionService : BookServiceBase, IIrmologionService
    {
        public IrmologionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public GetTheotokionResponse GetTheotokion(GetTheotokionRequest request)
        {
            GetTheotokionResponse response = new GetTheotokionResponse();

            //_unitOfWork.Repository<>
            throw new NotImplementedException();
        }
    }
}
