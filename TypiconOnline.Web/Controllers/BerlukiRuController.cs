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

namespace TypiconOnline.Web.Controllers
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
            return Get(DateTime.Now);
        }

        [Route("Get/{date}/{language?}")]
        public ActionResult Get(DateTime date, string language = "cs-ru")
        {
            return GetHtmlString(date, language);
        }

        private ActionResult GetHtmlString(DateTime date, string language)
        {
            try
            {
                if ((date.DayOfWeek == DayOfWeek.Sunday) && (date.Hour > 17))
                {
                    date = date.AddDays(1);
                }

                string resultString = "";

                var week = _outputForms.GetWeek(TYPICON_ID, date, language);

                week.OnSuccess(() =>
                {
                    resultString = _weekViewer.Execute(week.Value);

                    var nextWeek = _outputForms.GetWeek(TYPICON_ID, date.AddDays(7), language);
                    nextWeek.OnSuccess(() =>
                    {
                        resultString += _weekViewer.Execute(nextWeek.Value);
                    })
                    .OnFailure(() => resultString = nextWeek.Error);
                })
                .OnFailure(() => resultString = week.Error);

                ViewBag.Schedule = new HtmlString(resultString);
            }
            catch
            {
                ViewBag.Schedule = new HtmlString(@"Произошла ошибка в формировании расписания. <a ref=""mailto:mail@berluki.ru"">Обратитесь</a> к администратору сайта.");
            }

            return View();
        }
    }
}
