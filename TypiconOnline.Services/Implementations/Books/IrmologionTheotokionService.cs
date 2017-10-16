using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Implementations.Books
{
    public class TheotokionAppService : TheotokionAppContext, ITheotokionAppService
    {
        public TheotokionAppService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public InsertTheotokionResponse InsertTheotokion(InsertTheotokionRequest request)
        {
            InsertTheotokionResponse resp = new InsertTheotokionResponse();

            try
            {
                if (request.Theotokion == null)
                {
                    resp.Exception = new ArgumentNullException("request.Theotokion");
                }
                else
                {
                    _unitOfWork.Repository<TheotokionApp>().Insert(request.Theotokion);
                }
            }
            catch (Exception ex)
            {
                resp.Exception = ex;
            }

            return resp;
        }

        public DeleteTheotokionResponse DeleteTheotokion(DeleteTheotokionRequest request)
        {
            throw new NotImplementedException();
        }

        

        public UpdateTheotokionResponse UpdateTheotokion(UpdateTheotokionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
