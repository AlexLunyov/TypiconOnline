using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Tests.Migration
{
    public class FakePsalterService : PsalterContext, IPsalterService
    {
        List<Psalm> psalmList = new List<Psalm>();

        public IEnumerable<Psalm> Psalms => psalmList;

        public FakePsalterService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override GetPsalmResponse Get(GetPsalmRequest request)
        {
            var response = base.Get(request);
            if (response.Exception == null)
            {
                if (!psalmList.Contains(response.Psalm))
                {
                    psalmList.Add(response.Psalm);
                }
            }
            return base.Get(request);
        }

        public RemovePsalmResponse Remove(RemovePsalmRequest request)
        {
            throw new NotImplementedException();
        }

        public AddPsalmResponse Add(AddPsalmRequest request)
        {
            var response = new AddPsalmResponse();

            try
            {
                psalmList.Add(request.Psalm);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public UpdatePsalmResponse Update(UpdatePsalmRequest request)
        {
            var response = new UpdatePsalmResponse();

            try
            {
                var psalm = psalmList.First(c => c.Number == request.Psalm.Number);
                //psalm.Number = request.Psalm.Number;
                psalm.Definition = request.Psalm.Definition;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }
    }
}
