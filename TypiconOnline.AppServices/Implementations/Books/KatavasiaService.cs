using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Implementations.Books
{
    public class KatavasiaService : KatavasiaContext, IKatavasiaService
    {
        public KatavasiaService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public AddKatavasiaResponse Insert(AddKatavasiaRequest request)
        {
            AddKatavasiaResponse resp = new AddKatavasiaResponse();

            try
            {
                if (request.Katavasia == null)
                {
                    resp.Exception = new ArgumentNullException("request.Katavasia");
                }
                else
                {
                    _unitOfWork.Repository<Katavasia>().Add(request.Katavasia);
                }
            }
            catch (Exception ex)
            {
                resp.Exception = ex;
            }

            return resp;
        }

        public RemoveKatavasiaResponse Delete(RemovePsalmRequest request)
        {
            throw new NotImplementedException();
        }

        public UpdateKatavasiaResponse Update(UpdateKatavasiaRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
