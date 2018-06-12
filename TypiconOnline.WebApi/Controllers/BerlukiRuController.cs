using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.AppServices.Implementations;
using System.Text;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Domain.Typicon;
using Microsoft.AspNetCore.Html;

namespace TypiconOnline.WebApi.Controllers
{
    [Route("[controller]")]
    public class BerlukiRuController : Controller
    {
        const int TYPICON_ID = 1;

        IScheduleService _scheduleService;
        IUnitOfWork unitOfWork;

        public BerlukiRuController(IUnitOfWork unitOfWork, IScheduleService scheduleService)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork in BerlukiRuController");
            _scheduleService = scheduleService ?? throw new ArgumentNullException("ScheduleService in BerlukiRuController");
        }

        // GET berlukiru
        [HttpGet]
        public ActionResult Index()
        {
            return GetHtmlString(DateTime.Now);
        }

        [HttpGet("{date}")]
        public ActionResult Get(DateTime date)
        {
            return GetHtmlString(date);
        }

        private ActionResult GetHtmlString(DateTime date)
        {
            if ((date.DayOfWeek == DayOfWeek.Sunday) && (date.Hour > 17))
            {
                date = date.AddDays(1);
            }

            var weekRequest = new GetScheduleWeekRequest()
            {
                Date = date,
                TypiconId = TYPICON_ID,
                Handler = new ScheduleHandler(),
                CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronomicDay)
            };

            var weekResponse = _scheduleService.GetScheduleWeek(weekRequest);

            var htmlViewer = new HtmlScheduleWeekViewer();
            htmlViewer.Execute(weekResponse.Week);

            string resultString = htmlViewer.ResultString;

            weekRequest.Date = date.AddDays(7);

            weekResponse = _scheduleService.GetScheduleWeek(weekRequest);
            htmlViewer.Execute(weekResponse.Week);
            resultString += htmlViewer.ResultString;

            ViewBag.Schedule = new HtmlString(resultString);

            return View();
        }
    }
}
