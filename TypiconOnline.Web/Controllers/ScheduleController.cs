using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Handlers.CustomParameters;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.WebQuery.OutputFiltering;
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
    [AllowAnonymous]
    public class ScheduleController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IScheduleWeekViewer<string> _weekViewer;
        private readonly IScheduleWeekViewer<Result<DocxToStreamWeekResponse>> _weekDownloadViewer;
        private readonly IScheduleDayViewer<string> dayViewer;

        public ScheduleController(IQueryProcessor queryProcessor
            , IScheduleDayViewer<string> dayViewer
            , IScheduleWeekViewer<string> weekViewer
            , IScheduleWeekViewer<Result<DocxToStreamWeekResponse>> weekDownloadViewer)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException("queryProcessor in ScheduleController");
            _weekViewer = weekViewer ?? throw new ArgumentNullException(nameof(weekViewer));
            _weekDownloadViewer = weekDownloadViewer ?? throw new ArgumentNullException(nameof(weekDownloadViewer));
            this.dayViewer = dayViewer ?? throw new ArgumentNullException("dayViewer in ScheduleController");
        }

        [Route("{id:int}/{date?}/{language?}")]
        [Breadcrumb("Устав")] 
        public IActionResult Index(int? id, DateTime date, string language)
        {
            if (id == null)
            {
                return NotFound();
            }
            var typicon = _queryProcessor.Process(new TypiconQuery(id.Value));

            if (typicon.Success)
            {
                ViewBag.TypiconName = typicon.Value.Name;
                ViewBag.Id = typicon.Value.Id;

                if (date == null || date == DateTime.MinValue)
                {
                    date = DateTime.Now;
                }

                ViewBag.Date = date;

                var weekResult = _queryProcessor.Process(new OutputWeekQuery(id.Value, date, new OutputFilter() { Language = language }));

                ViewBag.Week = weekResult;
            }

            return View();
        }

        // GET api/<controller>/getweek/1/01-01-2019
        //[HttpPost]
        [Route("{id}/{date?}/{language?}")]
        public IActionResult Download(int? id, DateTime date, string language)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (date == null || date == DateTime.MinValue)
            {
                date = DateTime.Now;
            }

            var weekResult = _queryProcessor.Process(new OutputWeekQuery(id.Value, date, new OutputFilter() { Language = language }));

            if (weekResult.Success)
            {
                var file = _weekDownloadViewer.Execute(id.Value, weekResult.Value);

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
        [Route("{id}/{worshipId}/{filter?}")]
        [Breadcrumb(Title = "Последовательность", FromAction = "Index")]
        public IActionResult Sequence(int id, int worshipId, OutputFilter filter)
        {
            if (filter == null)
            {
                filter = new OutputFilter() { Language = "cs-ru" };
            }

            var worshipResult = _queryProcessor.Process(new OutputWorshipQuery(worshipId, filter));

            return View(worshipResult);
        }
    }
}