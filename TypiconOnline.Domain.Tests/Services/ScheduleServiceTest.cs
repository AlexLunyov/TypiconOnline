using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Services;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.Domain.Tests.Services
{
    [TestFixture]
    public class ScheduleServiceTest
    {
        [Test]
        public void ScheduleService_GetService()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            BookStorage.Instance = BookStorageFactory.Create();

            TypiconEntity typiconEntity = _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            ScheduleHandler handler = new ScheduleHandler();

            GetScheduleDayRequest request = new GetScheduleDayRequest()
            {
                Date = new DateTime(2018, 5, 21),//DateTime.Today,
                Mode = HandlingMode.AstronimicDay,
                Handler = new ScheduleHandler(),
                Typicon = typiconEntity
            };

            ScheduleService scheduleService = ScheduleServiceFactory.Create();

            GetScheduleDayResponse response = scheduleService.GetScheduleDay(request);

            _unitOfWork.Commit();

            Assert.AreEqual(3, response.Day.Schedule.ChildElements.Count);

            StringBuilder builder = new StringBuilder();

            builder.AppendLine(response.Day.Date.ToShortDateString() + " " + response.Day.Name);

            foreach (WorshipRuleViewModel service in response.Day.Schedule.ChildElements)
            {
                builder.AppendLine(service.Time + " " + service.Text);
            }

            Assert.Pass(builder.ToString());
        }
    }
}
