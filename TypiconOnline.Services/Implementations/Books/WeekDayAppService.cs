using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Implementations.Books
{
    public class WeekDayAppService : WeekDayAppContext, IWeekDayAppService
    {
        public WeekDayAppService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public AddWeekDayAppResponse Insert(AddWeekDayAppRequest request)
        {
            var resp = new AddWeekDayAppResponse();

            try
            {
                if (request.WeekDayApp == null)
                {
                    resp.Exception = new ArgumentNullException("request.WeekDayApp");
                }
                else
                {
                    _unitOfWork.Repository<WeekDayApp>().Add(request.WeekDayApp);
                }
            }
            catch (Exception ex)
            {
                resp.Exception = ex;
            }

            return resp;
        }


        public UpdateWeekDayAppResponse Update(UpdateWeekDayAppRequest request)
        {
            throw new NotImplementedException();
        }

        public RemoveWeekDayAppResponse Delete(RemoveWeekDayAppRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
