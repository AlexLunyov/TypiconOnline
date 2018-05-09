using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Implementations.Books
{
    public class OktoikhDayService : OktoikhContext, IOktoikhDayService
    {
        public OktoikhDayService(IUnitOfWork unitOfWork, IEasterContext easterContext) : base(unitOfWork, easterContext) { }

        public RemoveOktoikhResponse Remove(RemoveOktoikhRequest request)
        {
            throw new NotImplementedException();
        }
                
        public AddOktoikhResponse Add(AddOktoikhRequest request)
        {
            AddOktoikhResponse resp = new AddOktoikhResponse();

            try
            {
                if (request.OktoikhDay == null)
                {
                    resp.Exception = new ArgumentNullException("request.OktoikhDay");
                }
                else
                {
                    _unitOfWork.Repository<OktoikhDay>().Add(request.OktoikhDay);
                }
            }
            catch (Exception ex)
            {
                resp.Exception = ex;
            }

            return resp;
        }

        public UpdateOktoikhResponse Update(UpdateOktoikhRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
