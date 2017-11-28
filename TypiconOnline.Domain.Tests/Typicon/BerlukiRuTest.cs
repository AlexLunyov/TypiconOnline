using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Services;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EF;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Books;

namespace TypiconOnline.Domain.Tests.Typicon
{
    [TestFixture]
    public class BerlukiRuTest
    {
        [Test]
        public void BerlukiRu_Test()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            BookStorage.Instance = BookStorageFactory.Create();

            TypiconEntity typiconEntity = _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            DateTime date = new DateTime(2017, 09, 03);//DateTime.Now;

            if ((date.DayOfWeek == DayOfWeek.Sunday) && (date.Hour > 17))
            {
                date = date.AddDays(1);
            }

            GetScheduleWeekRequest weekRequest = new GetScheduleWeekRequest()
            {
                Date = date,
                Typicon = typiconEntity,
                Mode = HandlingMode.AstronimicDay,
                Handler = new ScheduleHandler()
            };

            ScheduleService scheduleService = ScheduleServiceFactory.Create();

            GetScheduleWeekResponse weekResponse = scheduleService.GetScheduleWeek(weekRequest);

            HtmlScheduleWeekViewer htmlViewer = new HtmlScheduleWeekViewer();
            htmlViewer.Execute(weekResponse.Week);

            string resultString = htmlViewer.ResultString;

            weekRequest.Date = date.AddDays(7);

            weekResponse = scheduleService.GetScheduleWeek(weekRequest);
            htmlViewer.Execute(weekResponse.Week);
            resultString += htmlViewer.ResultString;

            Assert.Pass(resultString);
        }

        [Test]
        public void BerlukiRu_ComparingRequests()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            BookStorage.Instance = BookStorageFactory.Create();

            TypiconEntity typiconEntity = _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            DateTime date = new DateTime(2017, 9, 24);

            //сначала как в запросе в контроллере BerlukiRuController
            GetScheduleDayRequest dayRequest1 = new GetScheduleDayRequest()
            {
                Date = date,
                Mode = HandlingMode.AstronimicDay,
                Handler = new ScheduleHandler(),
                Typicon = typiconEntity,
                CustomParameters = new List<IScheduleCustomParameter>(),
            };

            ScheduleService scheduleService = ScheduleServiceFactory.Create();

            GetScheduleDayResponse dayResponse1 = scheduleService.GetScheduleDay(dayRequest1);

            //теперь как TyppiconController
            GetScheduleDayRequest dayRequest2 = new GetScheduleDayRequest()
            {
                Date = date,
                Typicon = typiconEntity,
                Handler = new ScheduleHandler(),
                Mode = HandlingMode.AstronimicDay,
                ConvertSignToHtmlBinding = true
            };

            GetScheduleDayResponse dayResponse2 = scheduleService.GetScheduleDay(dayRequest2);

            GetScheduleWeekRequest weekRequest = new GetScheduleWeekRequest()
            {
                Date = date,
                Typicon = typiconEntity,
                Mode = HandlingMode.AstronimicDay,
                Handler = new ScheduleHandler()
            };

            GetScheduleWeekResponse weekResponse = scheduleService.GetScheduleWeek(weekRequest);

            Assert.AreEqual(dayResponse1.Day.Name, dayResponse2.Day.Name);
            Assert.AreEqual(dayResponse1.Day.Name, weekResponse.Week.Days.Last().Name);
        }
    }
}
