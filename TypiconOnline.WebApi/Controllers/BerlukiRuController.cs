using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Services;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.AppServices.Implementations;
using System.Text;

namespace TypiconOnline.WebApi.Controllers
{
    [Route("[controller]")]
    public class BerlukiRuController : Controller
    {
        ITypiconEntityService _typiconEntityService;
        IScheduleService _scheduleService;

        public BerlukiRuController(ITypiconEntityService typiconEntityService, IScheduleService scheduleService)
        {
            _typiconEntityService = typiconEntityService ?? throw new ArgumentNullException("TypiconEntityService in BerlukiRuController");
            _scheduleService = scheduleService ?? throw new ArgumentNullException("ScheduleService in BerlukiRuController");
        }

        // GET berlukiru
        [HttpGet]
        public ContentResult Index()
        {
            try
            {
                return Content(GetHtmlString(DateTime.Now), "text/html", Encoding.UTF8);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        [HttpGet("{date}")]
        public ContentResult Get(DateTime date)
        {
            return Content(GetHtmlString(date), "text/html", Encoding.UTF8);
        }

        private string GetHtmlString(DateTime date)
        {
            var response = _typiconEntityService.GetTypiconEntity(1);
            
            if ((date.DayOfWeek == DayOfWeek.Sunday) && (date.Hour > 17))
            {
                date = date.AddDays(1);
            }

            var weekRequest = new GetScheduleWeekRequest()
            {
                Date = date,
                Typicon = response.TypiconEntity,
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

            _typiconEntityService.UpdateTypiconEntity(new UpdateTypiconEntityRequest() { TypiconEntity = response.TypiconEntity });

            return resultString;
        }
    }
}
