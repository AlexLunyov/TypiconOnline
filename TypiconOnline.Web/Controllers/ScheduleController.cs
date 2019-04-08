using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Web.Services;

namespace TypiconOnline.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]/[action]")]
    public class ScheduleController : Controller
    {
        private readonly IDataQueryProcessor queryProcessor;
        private readonly IOutputForms _outputForms;
        private readonly IScheduleWeekViewer<string> _weekViewer;
        private readonly IScheduleWeekViewer<Result<DocxToStreamWeekResponse>> _weekDownloadViewer;
        private readonly IScheduleDayViewer<string> dayViewer;

        public ScheduleController(IDataQueryProcessor queryProcessor, IOutputForms outputForms, IScheduleDayViewer<string> dayViewer
            , IScheduleWeekViewer<string> weekViewer
            , IScheduleWeekViewer<Result<DocxToStreamWeekResponse>> weekDownloadViewer)
        {
            this.queryProcessor = queryProcessor ?? throw new ArgumentNullException("queryProcessor in ScheduleController");
            _outputForms = outputForms ?? throw new ArgumentNullException(nameof(outputForms));
            _weekViewer = weekViewer ?? throw new ArgumentNullException(nameof(weekViewer));
            _weekDownloadViewer = weekDownloadViewer ?? throw new ArgumentNullException(nameof(weekDownloadViewer));
            this.dayViewer = dayViewer ?? throw new ArgumentNullException("dayViewer in ScheduleController");
        }

        [Route("{id:int}/{date?}/{language?}")]
        [Breadcrumb("Устав")] 
        public IActionResult Index(int id, DateTime date, string language)
        {
            var typicon = queryProcessor.Process(new TypiconQuery(id));

            if (typicon.Success)
            {
                ViewBag.TypiconName = typicon.Value.Name;
                ViewBag.Id = typicon.Value.Id;

                if (date == null || date == DateTime.MinValue)
                {
                    date = DateTime.Now;
                }

                ViewBag.Date = date.ToString("dd-MM-yyyy");

                var weekResult = _outputForms.GetWeek(id, date, language);

                ViewBag.Week = weekResult;
            }

            return View();
        }

        // GET api/<controller>/getweek/1/01-01-2019
        //[HttpPost]
        [Route("{id}/{date?}/{language?}")]
        public IActionResult Download(int id, DateTime date, string language)
        {
            if (date == null || date == DateTime.MinValue)
            {
                date = DateTime.Now;
            }

            var weekResult = _outputForms.GetWeek(id, date, language);

            if (weekResult.Success)
            {
                var file = _weekDownloadViewer.Execute(weekResult.Value);

                if (file.Success)
                {
                    return File(file.Value.Content, file.Value.ContentType, file.Value.FileDownloadName);
                }
                else
                {
                    //error
                }
            }
            else
            {
                //return weekResult.Error;
            }

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">TypiconEntity id</param>
        /// <param name="date">Дата</param>
        /// <param name="w">Индекс службы</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/{date}/{w?}/{sequenceParams?}")]
        [Breadcrumb(Title = "Последовательность", FromAction = "Index")]
        public IActionResult Sequence(int id, DateTime date, int? w, SequenceParams sequenceParams)
        {
            string language = "cs-ru";

            if (date == null || date == DateTime.MinValue)
            {
                date = DateTime.Now;
            }

            if (string.IsNullOrEmpty(language))
            {
                language = "cs-ru";
            }

            var dayResult = _outputForms.Get(id, date, language);

            string result = "";

            if (dayResult.Success)
            {
                if (w != null && dayResult.Value[(int)w] is LocalizedOutputWorship model)
                {
                    result = dayViewer.Execute(model);
                }
                else
                {
                    result = dayViewer.Execute(dayResult.Value);
                }
            }
            else
            {
                result = dayResult.Error;
            }

            ViewBag.Sequence = new HtmlString(result);

            return View();
        }
    }
}