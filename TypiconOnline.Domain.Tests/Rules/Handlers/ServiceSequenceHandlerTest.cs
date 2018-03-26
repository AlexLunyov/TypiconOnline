using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Services;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EF;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;

namespace TypiconOnline.Domain.Tests.Rules.Handlers
{
    [TestFixture]
    public class ServiceSequenceHandlerTest
    {
        [Test]
        public void ServiceSequenceHandler_Working()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            //BookStorage.Instance = BookStorageFactory.Create();

            TypiconEntity typiconEntity = _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            GetScheduleDayRequest request = new GetScheduleDayRequest()
            {
                Date = new DateTime(2018, 5, 21),//DateTime.Today,
                Handler = new ServiceSequenceHandler(),
                Typicon = typiconEntity,
                CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronimicDay)
            };

            ScheduleService scheduleService = ScheduleServiceFactory.Create();

            GetScheduleDayResponse response = scheduleService.GetScheduleDay(request);
        }
    }
}
