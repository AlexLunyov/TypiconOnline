using System;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.TheotokionApp
{
    /// <summary>
    /// Служба позволяет получить объектную модель Богородична из Ирмология
    /// </summary>
    public class TheotokionAppContext : BookServiceBase, ITheotokionAppContext
    {
        public TheotokionAppContext(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public GetTheotokionResponse Get(GetTheotokionRequest request)
        {
            GetTheotokionResponse response = new GetTheotokionResponse();

            try
            {
                TheotokionApp theotokion = _unitOfWork.Repository<TheotokionApp>()
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
