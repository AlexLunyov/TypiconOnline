using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypiconOnline.Domain.Command.Typicon;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Web.Extensions;
using TypiconOnline.WebServices.Authorization;

namespace TypiconOnline.Web.Controllers
{
    //[Authorize(Roles = RoleConstants.AdminAndEditorRoles)]
    public class MenologyRuleController : TypiconChildBaseController<MenologyRuleGridModel, MenologyRule>
    {
        private const string DEFAULT_LANGUAGE = "cs-ru";
        //private readonly IDataQueryProcessor _queryProcessor;
        //private readonly IAuthorizationService _authorizationService;


        public MenologyRuleController(
            IQueryProcessor queryProcessor,
            IAuthorizationService authorizationService,
            ICommandProcessor commandProcessor) : base(queryProcessor, authorizationService, commandProcessor)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">MenologyRuleId</param>
        /// <param name="fromSchedule">Признак того, что пришли из графика богослужений</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id, bool fromSchedule = false)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var typiconEntity = QueryProcessor.Process(new TypiconEntityByMenologyRuleQuery(id));

            if (typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                ViewBag.Signs = QueryProcessor.GetSigns(typiconEntity.Value.Id, DEFAULT_LANGUAGE);
                ViewBag.TypiconId = typiconEntity.Value.Id.ToString();

                ViewBag.IsFromSchedule = fromSchedule;

                var found = QueryProcessor.Process(new MenologyRuleEditQuery(id, DEFAULT_LANGUAGE));

                if (found.Success)
                {
                    return View(found.Value);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenologyRuleEditModel model)
        {
            var typiconEntity = QueryProcessor.Process(new TypiconEntityByMenologyRuleQuery(model.Id));

            if (ModelState.IsValid
                && typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                var command = new EditMenologyRuleCommand(model.Id,
                    model.DayWorships.Select(c => (c.WorshipId, c.Order)),
                    model.TemplateId,
                    model.IsAddition,
                    model.RuleDefinition,
                    model.ModRuleDefinition);

                await CommandProcessor.ExecuteAsync(command);

                return (ViewBag.IsFromSchedule is bool b == true)
                                ? RedirectToAction(nameof(ScheduleSettingsController.Index), "ScheduleSettings", new { id = typiconEntity.Value.Id })
                                : RedirectToAction(nameof(Index), new { id = typiconEntity.Value.Id });
            }

            ViewBag.Signs = QueryProcessor.GetSigns(typiconEntity.Value.Id, DEFAULT_LANGUAGE);
            ViewBag.TypiconId = typiconEntity.Value.Id.ToString();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var typiconEntity = QueryProcessor.Process(new TypiconEntityQuery(id));

            if (typiconEntity != null
                && IsAuthorizedToEdit(typiconEntity))
            {
                ViewBag.Signs = QueryProcessor.GetSigns(typiconEntity.Id, DEFAULT_LANGUAGE);

                return View(new MenologyRuleCreateModel());
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenologyRuleCreateModel model)
        {
            var typiconEntity = QueryProcessor.Process(new TypiconEntityQuery(model.Id));

            if (ModelState.IsValid
                && typiconEntity != null
                && IsAuthorizedToEdit(typiconEntity))
            {
                var command = new CreateMenologyRuleCommand(model.Id,
                    model.DayWorships.Select(c => (c.WorshipId, c.Order)),
                    model.TemplateId,
                    model.IsAddition,
                    model.RuleDefinition,
                    model.ModRuleDefinition);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index), new { id = typiconEntity.Id });
            }

            ViewBag.Signs = QueryProcessor.GetSigns(typiconEntity.Id, DEFAULT_LANGUAGE);

            return View(model);
        }

        [Route("[controller]/[action]/{date?}")]
        public IActionResult GetDayWorships(DateTime? date)
        {
            var result = QueryProcessor.Process(new MenologyDayWorshipQuery(date, DEFAULT_LANGUAGE));

            if (result.Success)
            {
                return Json(new { data = result.Value.ToList() });
            }
            else
            {
                return NotFound();
            }
        }

        #region Overrides
        
        protected override IGridQuery<MenologyRuleGridModel> GetQuery(int id) => new AllMenologyRulesWebQuery(id, DEFAULT_LANGUAGE);

        protected override TypiconEntityByChildQuery<MenologyRule> GetTypiconEntityByChildQuery(int id)
            => new TypiconEntityByMenologyRuleQuery(id);

        protected override DeleteRuleCommandBase<MenologyRule> GetDeleteCommand(int id)
            => new DeleteMenologyRuleCommand(id);

        #endregion
    }
}
