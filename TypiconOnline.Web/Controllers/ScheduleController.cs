using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class ScheduleController : Controller
    {
        private const string DEFAULT_LANGUAGE = "cs-ru";

        private readonly IQueryProcessor _queryProcessor;
        private readonly IScheduleWeekViewer<string> _weekViewer;
        private readonly IScheduleWeekViewer<Result<DocxToStreamWeekResponse>> _weekDownloadViewer;
        private readonly IScheduleDayViewer<string> dayViewer;

        public ScheduleController(IQueryProcessor queryProcessor
            , IScheduleDayViewer<string> dayViewer
            , IScheduleWeekViewer<string> weekViewer
            , IScheduleWeekViewer<Result<DocxToStreamWeekResponse>> weekDownloadViewer)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _weekViewer = weekViewer ?? throw new ArgumentNullException(nameof(weekViewer));
            _weekDownloadViewer = weekDownloadViewer ?? throw new ArgumentNullException(nameof(weekDownloadViewer));
            this.dayViewer = dayViewer ?? throw new ArgumentNullException("dayViewer in ScheduleController");
        }

        [Route("{id:int}/{date?}/{language?}")]
        //[Breadcrumb("Устав")] 
        public IActionResult Index(int? id, DateTime date, string language = DEFAULT_LANGUAGE)
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

                return View();
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<controller>/getweek/1/01-01-2019
        //[HttpPost]
        [Route("{id}/{date?}/{language?}")]
        public IActionResult Download(int? id, DateTime date, string language = DEFAULT_LANGUAGE)
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
        //[Breadcrumb(Title = "Последовательность", FromAction = "Index")]
        public IActionResult Sequence(int id, int worshipId, OutputFilter filter)
        {
            if (filter == null)
            {
                filter = new OutputFilter() { Language = "cs-ru" };
            }

            var worshipResult = _queryProcessor.Process(new OutputWorshipQuery(worshipId, filter));

            return View(worshipResult);
        }

        /// <summary>
        /// Возвращает Json с расписанием
        /// </summary>
        /// <param name="id">Is Устава</param>
        /// <param name="weeksCount">Количество недель для расписания</param>
        /// <param name="language">Язык локализации</param>
        /// <returns></returns>
        [HttpGet]
        [EnableCors("GetSchedulePolicy")]
        [Route("{id}/{weeksCount?}/{language?}")]
        public IActionResult Get(int id, int? weeksCount = 2, string language = DEFAULT_LANGUAGE)
        {
            try
            {
                var date = DateTime.Now;
                if ((date.DayOfWeek == DayOfWeek.Sunday) && (date.Hour > 17))
                {
                    date = date.AddDays(1);
                }

                if (weeksCount < 0)
                {
                    weeksCount = 2;
                }
                if (weeksCount > 5)
                {
                    weeksCount = 5;
                }

                var weeks = new List<Result<FilteredOutputWeek>>();

                for (int i = 0; i < weeksCount; i++)
                {
                    var week = _queryProcessor.Process(new OutputWeekQuery(id, date, new OutputFilter() { Language = language }));

                    weeks.Add(week);

                    date = date.AddDays(7);
                }

                object result = null;

                Result.Combine(weeks.ToArray<Result>())
                    .OnSuccess(() =>
                    {
                        result = new { schedule = weeks.ToArray<Result>() };
                    })
                    .OnFailure(err => result = new { err });

                return Json(result);
            }
            catch //(Exception ex)
            {
                return Json(new { error = "Системная ошибка." });
            }
        }
    }
}