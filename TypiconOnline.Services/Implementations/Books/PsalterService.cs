using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Implementations.Books
{
    public class PsalterService : PsalterContext, IPsalterService
    {
        public PsalterService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public DeletePsalmResponse Delete(DeletePsalmRequest request)
        {
            throw new NotImplementedException();
        }

        public InsertPsalmResponse Insert(InsertPsalmRequest request)
        {
            var resp = new InsertPsalmResponse();
            try
            {
                if (request.Psalm == null)
                {
                    resp.Exception = new ArgumentNullException("InsertPsalmRequest.Psalm");
                }
                else
                {
                    _unitOfWork.Repository<Psalm>().Insert(request.Psalm);
                }
            }
            catch (Exception ex)
            {
                resp.Exception = ex;
            }
            return resp;
        }

        public UpdatePsalmResponse Update(UpdatePsalmRequest request)
        {
            var resp = new UpdatePsalmResponse();
            try
            {
                if (request.Psalm == null)
                {
                    resp.Exception = new ArgumentNullException("UpdatePsalmRequest.Psalm");
                }
                else
                {
                    _unitOfWork.Repository<Psalm>().Update(request.Psalm);
                    //_unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                resp.Exception = ex;
            }
            return resp;
        }

        public void DeleteAll()
        {
            foreach (var psalm in _unitOfWork.Repository<Psalm>().GetAll())
            {
                _unitOfWork.Repository<Psalm>().Delete(psalm);
            }
            
        }
    }
}
