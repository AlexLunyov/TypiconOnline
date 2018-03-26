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
                return Content(GetHtmlString(), "text/html", Encoding.UTF8);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            try
            {
                var response = _typiconEntityService.GetTypiconEntity(1);

                return response.TypiconEntity.Name;

                var date = DateTime.Now;

                if ((date.DayOfWeek == DayOfWeek.Sunday) && (date.Hour > 17))
                {
                    date = date.AddDays(1);
                }

                var weekRequest = new GetScheduleWeekRequest()
                {
                    Date = date,
                    Typicon = response.TypiconEntity,
                    Handler = new ScheduleHandler(),
                    CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronimicDay)
                };

                var weekResponse = _scheduleService.GetScheduleWeek(weekRequest);

                return weekResponse.Week.Name;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            //var htmlViewer = new HtmlScheduleWeekViewer();
            //htmlViewer.Execute(weekResponse.Week);

            //string resultString = htmlViewer.ResultString;

            //return resultString;
        }

        private string GetHtmlString()
        {
            var response = _typiconEntityService.GetTypiconEntity(1);

            var date = DateTime.Now;

            if ((date.DayOfWeek == DayOfWeek.Sunday) && (date.Hour > 17))
            {
                date = date.AddDays(1);
            }

            var weekRequest = new GetScheduleWeekRequest()
            {
                Date = date,
                Typicon = response.TypiconEntity,
                Handler = new ScheduleHandler(),
                CheckParameters = new CustomParamsCollection<IRuleCheckParameter>().SetModeParam(HandlingMode.AstronimicDay)
            };

            var weekResponse = _scheduleService.GetScheduleWeek(weekRequest);

            var htmlViewer = new HtmlScheduleWeekViewer();
            htmlViewer.Execute(weekResponse.Week);

            string resultString = htmlViewer.ResultString;

            weekRequest.Date = date.AddDays(7);

            weekResponse = _scheduleService.GetScheduleWeek(weekRequest);
            htmlViewer.Execute(weekResponse.Week);
            resultString += htmlViewer.ResultString;

            return resultString;
        }
    }
}
