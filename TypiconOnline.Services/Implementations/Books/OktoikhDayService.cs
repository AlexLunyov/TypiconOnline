using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Implementations.Books
{
    public class OktoikhDayService : OktoikhContext, IOktoikhDayService
    {
        public OktoikhDayService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public DeleteOktoikhResponse DeleteOktoikh(DeleteOktoikhRequest request)
        {
            throw new NotImplementedException();
        }
                
        public InsertOktoikhResponse InsertOktoikh(InsertOktoikhRequest request)
        {
            InsertOktoikhResponse resp = new InsertOktoikhResponse();

            try
            {
                if (request.OktoikhDay == null)
                {
                    resp.Exception = new ArgumentNullException("request.OktoikhDay");
                }
                else
                {
                    _unitOfWork.Repository<OktoikhDay>().Insert(request.OktoikhDay);
                }
            }
            catch (Exception ex)
            {
                resp.Exception = ex;
            }

            return resp;
        }

        public UpdateOktoikhResponse UpdateOktoikh(UpdateOktoikhRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
