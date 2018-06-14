using System;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Katavasia
{
    public class KatavasiaContext : BookServiceBase, IKatavasiaContext
    {
        public KatavasiaContext(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public GetKatavasiaResponse Get(GetKatavasiaRequest request)
        {
            GetKatavasiaResponse response = new GetKatavasiaResponse();

            try
            {
                Katavasia katavasia = _unitOfWork.Repository<Katavasia>()
                                            .Get(c => c.Name == request.Name);

                response.BookElement = katavasia.GetElement();
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public GetAllKatavasiaResponse GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
