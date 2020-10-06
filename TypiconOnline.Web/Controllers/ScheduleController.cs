using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Common;
using TypiconOnline.Domain.Command.Typicon;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Web.Models.ScheduleViewModels;
using TypiconOnline.WebServices.Authorization;
using TypiconOnline.Web.Extensions;

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

        private const string HTML = "html";
        private const string JSON = "json";

        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandProcessor _commandProcessor;
        private readonly IScheduleWeekViewer<string> _weekViewer;
        private readonly IScheduleWeekViewer<Result<FileDownloadResponse>> _weekDownloadViewer;
        private readonly IAuthorizationService _authorizationService;
        //private readonly IScheduleDayViewer<string> dayViewer;

        public ScheduleController(IQueryProcessor queryProcessor
            , ICommandProcessor commandProcessor
            //, IScheduleDayViewer<string> dayViewer
            , IScheduleWeekViewer<string> weekViewer
            , IScheduleWeekViewer<Result<FileDownloadResponse>> weekDownloadViewer
            , IAuthorizationService authorizationService)
        {
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));
            _weekViewer = weekViewer ?? throw new ArgumentNullException(nameof(weekViewer));
            _weekDownloadViewer = weekDownloadViewer ?? throw new ArgumentNullException(nameof(weekDownloadViewer));
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            //this.dayViewer = dayViewer ?? throw new ArgumentNullException("dayViewer in ScheduleController");
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
                if (date == null || date == DateTime.MinValue)
                {
                    date = DateTime.Now;
                }

                return View(new ScheduleViewModel()
                {
                    Id = typicon.Value.Id,
                    Date = date,
                    Language = language,
                    Name = typicon.Value.Name,
                });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult View(int id, DateTime date, string language = DEFAULT_LANGUAGE)
        {
            //является ли редактором
            bool canEdit = false;
            try
            {
                var typiconEntity = _queryProcessor.Process(new TypiconEntityQuery(id));

                canEdit = _authorizationService.AuthorizeAsync(
                                                           User, typiconEntity,
                                                           TypiconOperations.Edit)
                                               .Result
                                               .Succeeded;
            }
            catch { }

            var weekResult = _queryProcessor.Process(new OutputWeekQuery(id, date, new OutputFilter() { Language = language }));

            object result = null;

            weekResult.OnSuccess(() =>
                {
                    result = new
                    {
                        isEditor = canEdit,
                        schedule = weekResult.Value
                    };
                })
                .OnFailure(err => result = new { err });

            return Json(result);

            //return PartialView("_View", weekResult);
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
        /// Возвращает Json с расписанием на указанное количество недель
        /// </summary>
        /// <param name="id">Is Устава</param>
        /// <param name="date">Дата</param>
        /// <param name="weeksCount">Количество недель для расписания</param>
        /// <param name="language">Язык локализации</param>
        /// <returns></returns>
        [HttpGet]
        [EnableCors("GetSchedulePolicy")]
        public IActionResult Week(string id, DateTime date, int? weeksCount, string language = DEFAULT_LANGUAGE)
        {
            if (date == null || date == DateTime.MinValue)
            {
                date = DateTime.Now;
            }

            var weeks = GetSchedule(id, date, weeksCount, language);

            object result = null;

            weeks.OnSuccess(() =>
                {
                    Result.Combine(weeks.Value.ToArray<Result>())
                        .OnSuccess(() =>
                        {
                            //Json
                            result = new 
                            { 
                                schedule = weeks
                                    .Value
                                    .Select(c => c.Value.Week) 
                            };
                        })
                        .OnFailure(err => result = new { err });
                })
                .OnFailure(err => result = new { err });

            return Json(result);
        }

        /// <summary>
        /// Возвращает html с расписанием
        /// </summary>
        /// <param name="id"></param>
        /// <param name="weeksCount"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        [HttpGet]
        [EnableCors("GetSchedulePolicy")]
        public IActionResult Html(string id, int? weeksCount = 2, string language = DEFAULT_LANGUAGE)
        {
            string resultString = "";

            var weeks = GetSchedule(id, DateTime.Now, weeksCount, language);

            weeks.OnSuccess(() =>
                {
                    Result.Combine(weeks.Value.ToArray<Result>())
                        .OnSuccess(() =>
                        {
                            //HTML
                            resultString = string
                                        .Join("", weeks.Value.Select(
                                            c => _weekViewer.Execute(c.Value.Id, c.Value.Week)));
                        })
                        .OnFailure(err => resultString = err);
                })
                .OnFailure(err => resultString = err);

            return Content(resultString, "text/html", Encoding.UTF8);
        }

        private Result<List<Result<(int Id, FilteredOutputWeek Week)>>> GetSchedule(string id, DateTime? d, int? weeksCount = 2, string language = DEFAULT_LANGUAGE)
        {
            try
            {
                //date
                var date = d ?? DateTime.Now;

                if ((date.DayOfWeek == DayOfWeek.Sunday) && (date.Hour > 17))
                {
                    date = date.AddDays(1);
                }

                //weeksCount
                if (!weeksCount.HasValue)
                {
                    weeksCount = 2;
                }

                if (weeksCount < 1 || weeksCount > 5)
                {
                    return Result.Fail<List<Result<(int Id, FilteredOutputWeek Week)>>>($"Параметр {nameof(weeksCount)} может принимать значения в диапазоне 1..5");
                }
                else
                {
                    //получаем коллекцию седмиц
                    var weeks = new List<Result<(int Id, FilteredOutputWeek Week)>>();

                    for (int i = 0; i < weeksCount; i++)
                    {
                        var week = _queryProcessor.Process(new OutputWeekBySystemNameQuery(id, date, weeksCount.Value, new OutputFilter() { Language = language }));

                        weeks.Add(week);

                        date = date.AddDays(7);
                    }

                    return Result.Ok(weeks);
                }
            }
            catch
            {
                return Result.Fail<List<Result<(int Id, FilteredOutputWeek Week)>>>(
                    @"Произошла ошибка в формировании расписания. <a ref=""mailto:admin@typicon.online"">Обратитесь</a> к администратору сайта.");
            }
        }

        /// <summary>
        /// Возвращает json с расписанием на указанный день
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        [HttpGet]
        [EnableCors("GetSchedulePolicy")]
        public IActionResult Day(string id, DateTime date, string language = DEFAULT_LANGUAGE)
        {
            if (date == null || date == DateTime.MinValue)
            {
                date = DateTime.Now;
            }

            var dayResult = _queryProcessor.Process(new OutputDayBySystemNameQuery(id, date, new OutputFilter() { Language = language }));

            object result = null;

            dayResult.OnSuccess(() =>
            {
                //Json
                result = new { day = dayResult.Value };
            })
            .OnFailure(err => result = new { err });

            return Json(result);
        }

        /// <summary>
        /// Редактирование выходной фомы дня
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [EnableCors("GetSchedulePolicy")]
        public IActionResult EditDay(int id)
        {
            try
            {
                var dayResult = _queryProcessor.Process(new OutputDayEditQuery(id));

                if (dayResult.Success)
                {
                    ViewBag.PrintTemplates = _queryProcessor.GetPrintTemplates(dayResult.Value.TypiconId, forDraft: false);
                }

                return PartialView("_EditDayPartial", dayResult.Value);
            }
            catch (SecurityException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditDay(OutputDayEditModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = new EditOutputDayCommand(model.Id
                        , model.Name
                        , model.NameStyle
                        , model.PrintTemplateId);

                    var result = await _commandProcessor.ExecuteAsync(command);

                    return Json(data: result.Error);
                }

                return Json(data: ModelState.Values);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Редактирование выходной формы службы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [EnableCors("GetSchedulePolicy")]
        public IActionResult EditWorship(int id)
        {
            try
            {
                var result = _queryProcessor.Process(new OutputWorshipEditQuery(id));

                return PartialView("_EditWorshipPartial", result.Value);
            }
            catch (SecurityException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditWorship(OutputWorshipEditModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = new EditOutputWorshipCommand(model.Id
                        , model.Name
                        , model.AdditionalName
                        , model.Time
                        , model.NameStyle
                        , model.AdditionalNameStyle);

                    var result = await _commandProcessor.ExecuteAsync(command);

                    return Json(data: result.Error);
                }

                return Json(data: ModelState.Values);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}