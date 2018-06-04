using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.WeekDayApp
{
    /// <summary>
    /// Предоставляет доступ к текстам на каждый день недели.
    /// </summary>
    public class WeekDayAppContext : BookServiceBase, IWeekDayAppContext
    {
        public WeekDayAppContext(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public GetWeekDayResponse Get(GetWeekDayRequest request)
        {
            var response = new GetWeekDayResponse();

            try
            {
                response.WeekDayApp = _unitOfWork.Repository<WeekDayApp>()
                                            .Get(c => c.DayOfWeek == request.DayOfWeek);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }
    }
}
