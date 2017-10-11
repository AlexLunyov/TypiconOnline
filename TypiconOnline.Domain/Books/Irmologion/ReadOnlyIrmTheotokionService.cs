using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Irmologion
{
    /// <summary>
    /// Служба позволяет получить объектную модель Богородична из Ирмология
    /// </summary>
    public class ReadOnlyIrmTheotokionService : BookServiceBase, IReadOnlyIrmTheotokionService
    {
        public ReadOnlyIrmTheotokionService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public GetTheotokionResponse GetTheotokion(GetTheotokionRequest request)
        {
            GetTheotokionResponse response = new GetTheotokionResponse();

            try
            {
                IrmologionTheotokion theotokion = _unitOfWork.Repository<IrmologionTheotokion>()
                                            .Get(c => c.Ihos == request.Ihos
                                                    && c.Place == request.Place
                                                    && c.DayOfWeek == request.DayOfWeek);

                YmnosGroup group = new YmnosGroup() { Ihos = theotokion.Ihos };
                group.Ymnis.Add(theotokion.GetElement());

                response.BookElement = group;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public GetAllTheotokionResponse GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
