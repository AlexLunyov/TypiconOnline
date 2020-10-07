using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Domain.Command.Typicon;
using TypiconOnline.Web.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TypiconOnline.Web.Controllers
{
    [Authorize(Roles = RoleConstants.AdminAndEditorRoles)]
    public class ScheduleSettingsController : BaseController
    {
        private readonly GridStore<SignGridModel> signStore;
        private readonly GridStore<MenologyRuleGridModel> menologyStore;
        private readonly GridStore<TriodionRuleGridModel> triodionStore;
        private readonly GridStore<DateGridItem> includedStore;

        public ScheduleSettingsController(
            IQueryProcessor queryProcessor,
            ICommandProcessor commandProcessor)
            : base(queryProcessor, commandProcessor)
        {
            signStore = new GridStore<SignGridModel>(queryProcessor
                , commandProcessor
                , this);

            menologyStore = new GridStore<MenologyRuleGridModel>(queryProcessor
                , commandProcessor
                , this
                //храним в сессии
                , storeToSession: true );

            triodionStore = new GridStore<TriodionRuleGridModel>(queryProcessor
                , commandProcessor
                , this
                //храним в сессии
                , storeToSession: true);

            includedStore = new GridStore<DateGridItem>(queryProcessor
                , commandProcessor
                , this);
        }

        // GET: /<controller>/
        public IActionResult Index(int id)
            => Perform(() => QueryProcessor.Process(new ScheduleWeekSettingsQuery(id)),
                       c => View(c.Value));

        [HttpGet]
        public IActionResult EditDays(int id)
            => Perform(() => QueryProcessor.Process(new ScheduleWeekSettingsQuery(id)),
                       c => PartialView("_EditDaysPartial", c.Value));

        [HttpPost]
        public IActionResult EditDays(ScheduleSettingsWeekDaysModel model)
            => Perform(() => CommandProcessor.Execute(new ScheduleSettingsWeekDaysCommand(model.TypiconId)
                                {
                                    IsMonday = model.IsMonday,
                                    IsTuesday = model.IsTuesday,
                                    IsWednesday = model.IsWednesday,
                                    IsThursday = model.IsThursday,
                                    IsFriday = model.IsFriday,
                                    IsSaturday = model.IsSaturday,
                                    IsSunday = model.IsSunday
                                }),
                       () => PartialView("_ViewDaysPartial", model));

        #region Sign

        public IActionResult LoadSigns(int id) => signStore.LoadGridData(new AllScheduleSignsQuery(id));

        /// <summary>
        /// загружаем таблицу знаков службы для добавления
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult LoadAddSignGrid(int id) => signStore.LoadGridData(new AllScheduleSignsToLoadQuery(id));

        [HttpGet]
        [Route("[controller]/[action]/{typiconId}")]
        public IActionResult AddSign(int typiconId)
            => Perform(() => QueryProcessor.Process(new ScheduleWeekSettingsQuery(typiconId)),
                       () => PartialView("_AddSignPartial"));

        [HttpPost]
        [Route("[controller]/[action]/{typiconId}/{ruleId}")]
        public IActionResult AddSign(int typiconId, int ruleId)
            => Perform(() => CommandProcessor.Execute(new AddScheduleSignCommand(typiconId, ruleId)),
                       () => Accepted());

        [HttpPost]
        [Route("[controller]/[action]/{ruleId}")]
        public IActionResult DeleteSign(int ruleId)
            => Perform(() => CommandProcessor.Execute(new DeleteScheduleSignCommand(ruleId)),
                       () => Accepted());

        #endregion

        #region MenologyRule

        public IActionResult LoadMenology(int id) => menologyStore.LoadGridData(new AllScheduleMenologyQuery(id));

        /// <summary>
        /// загружаем таблицу знаков службы для добавления
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[controller]/[action]/{id}/{date?}")]
        public IActionResult LoadMenologyToAddGrid(int id, DateTime? date) => menologyStore.LoadGridData(new AllScheduleMenologyToAddQuery(id, date));

        [HttpGet]
        [Route("[controller]/[action]/{typiconId}")]
        public IActionResult AddMenology(int typiconId)
            => Perform(() => QueryProcessor.Process(new ScheduleWeekSettingsQuery(typiconId)),
                       () => PartialView("_AddMenologyPartial"));

        [HttpPost]
        [Route("[controller]/[action]/{typiconId}/{ruleId}")]
        public IActionResult AddMenology(int typiconId, int ruleId)
            => Perform(() => CommandProcessor.Execute(new AddScheduleMenologyCommand(typiconId, ruleId)),
                       () => Accepted());

        [HttpPost]
        [Route("[controller]/[action]/{ruleId}")]
        public IActionResult DeleteMenology(int ruleId)
            => Perform(() => CommandProcessor.Execute(new DeleteScheduleMenologyCommand(ruleId)),
                       () => Accepted());

        #endregion

        #region TriodionRule

        public IActionResult LoadTriodion(int id) => triodionStore.LoadGridData(new AllScheduleTriodionQuery(id));

        /// <summary>
        /// загружаем таблицу для добавления
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[controller]/[action]/{id}/{days?}")]
        public IActionResult LoadTriodionToAddGrid(int id, int? days) => triodionStore.LoadGridData(new AllScheduleTriodionToAddQuery(id, days));

        [HttpGet]
        [Route("[controller]/[action]/{typiconId}")]
        public IActionResult AddTriodion(int typiconId)
            => Perform(() => QueryProcessor.Process(new ScheduleWeekSettingsQuery(typiconId)),
                       () => PartialView("_AddTriodionPartial"));

        [HttpPost]
        [Route("[controller]/[action]/{typiconId}/{ruleId}")]
        public IActionResult AddTriodion(int typiconId, int ruleId)
            => Perform(() => CommandProcessor.Execute(new AddScheduleTriodionCommand(typiconId, ruleId)),
                       () => Accepted());

        [HttpPost]
        [Route("[controller]/[action]/{ruleId}")]
        public IActionResult DeleteTriodion(int ruleId)
            => Perform(() => CommandProcessor.Execute(new DeleteScheduleTriodionCommand(ruleId)),
                       () => Accepted());

        #endregion

        //#region IncludedDates

        //public IActionResult LoadIncludedDates(int id) => includedStore.LoadGridData(new AllScheduleIncludedDatesQuery(id));

        /////// <summary>
        /////// загружаем таблицу для добавления
        /////// </summary>
        /////// <param name="id"></param>
        /////// <returns></returns>
        ////[Route("[controller]/[action]/{id}/{days?}")]
        ////public IActionResult LoadTriodionToAddGrid(int id, int? days) => triodionStore.LoadGridData(new AllScheduleTriodionToAddQuery(id, days));

        //[HttpGet]
        //[Route("[controller]/[action]/{typiconId}")]
        //public IActionResult ShowIncludedDateToAdd(int typiconId)
        //    => Perform(() => QueryProcessor.Process(new ScheduleIncludedDatesQuery(typiconId)),
        //               () => PartialView("_AddTriodionPartial"));

        //[HttpPost]
        //[Route("[controller]/[action]/{typiconId}/{date}")]
        //public IActionResult AddIncludedDate(int typiconId, DateTime date)
        //    => Perform(() => CommandProcessor.Execute(new AddScheduleIncludedDateCommand(typiconId, date)),
        //               () => Accepted());

        //[HttpPost]
        //[Route("[controller]/[action]/{date}")]
        //public IActionResult DeleteIncludedDate(int typiconId, DateTime date)
        //    => Perform(() => CommandProcessor.Execute(new DeleteScheduleIncludedDateCommand(typiconId, date)),
        //               () => Accepted());

        //#endregion

        
    }
}
