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
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.WebApi.Controllers
{
    [Route("[controller]")]
    public class BerlukiRuController : Controller
    {
        const int TYPICON_ID = 1;

        private readonly IOutputForms _outputForms;
        private readonly IScheduleWeekViewer<string> _weekViewer = new HtmlScheduleWeekViewer();

        public BerlukiRuController(IOutputForms outputForms)//, IScheduleWeekViewer<string> weekViewer)
        {
            _outputForms = outputForms ?? throw new ArgumentNullException(nameof(outputForms));
            //_weekViewer = weekViewer ?? throw new ArgumentNullException(nameof(weekViewer));
        }

        // GET berlukiru
        [HttpGet]
        public ActionResult Index()
        {
            return GetHtmlString(DateTime.Now);
        }

        [Route("Get/{date}")]
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

            var week = _outputForms.GetWeek(TYPICON_ID, date);
            var nextWeek = _outputForms.GetWeek(TYPICON_ID, date.AddDays(7));

            Result.Combine(week, nextWeek)
                .OnSuccess(() =>
                {
                    string resultString = _weekViewer.Execute(week.Value) + _weekViewer.Execute(nextWeek.Value);

                    ViewBag.Schedule = new HtmlString(resultString);
                })
                .OnFailure(() =>
                {
                    string msg = string.Empty;

                    week.OnFailure(() => msg += week.Error);
                    nextWeek.OnFailure(() => msg += nextWeek.Error);

                    ViewBag.Schedule = msg;
                });

            return View();
        }
    }
}
