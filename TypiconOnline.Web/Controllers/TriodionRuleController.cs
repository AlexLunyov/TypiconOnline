﻿using System;
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
    public class TriodionRuleController : TypiconChildBaseController<TriodionRuleGridModel, TriodionRule>
    {
        private const string DEFAULT_LANGUAGE = "cs-ru";
        //private readonly IDataQueryProcessor _queryProcessor;
        //private readonly IAuthorizationService _authorizationService;


        public TriodionRuleController(
            IQueryProcessor queryProcessor,
            IAuthorizationService authorizationService,
            ICommandProcessor commandProcessor) : base(queryProcessor, authorizationService, commandProcessor)
        {
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">TriodionRuleId</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var typiconEntity = QueryProcessor.Process(new TypiconEntityByTriodionRuleQuery(id));

            if (typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                ViewBag.Signs = QueryProcessor.GetSigns(typiconEntity.Value.Id, DEFAULT_LANGUAGE);
                ViewBag.TypiconId = typiconEntity.Value.Id;

                var found = QueryProcessor.Process(new TriodionRuleEditQuery(id, DEFAULT_LANGUAGE));

                if (found.Success)
                {
                    return View(found.Value);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TriodionRuleCreateEditModel model)
        {
            var typiconEntity = QueryProcessor.Process(new TypiconEntityByTriodionRuleQuery(model.Id));

            model.Mode = TriodionRuleModelMode.Edit;

            if (ModelState.IsValid
                && typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                var command = new EditTriodionRuleCommand(model.Id,
                    model.DayWorships.Select(c => (c.WorshipId, c.Order)),
                    model.TemplateId,
                    model.DaysFromEaster,
                    model.IsTransparent,
                    model.RuleDefinition,
                    model.ModRuleDefinition);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index), new { id = typiconEntity.Value.Id });
            }

            ViewBag.Signs = QueryProcessor.GetSigns(typiconEntity.Value.Id, DEFAULT_LANGUAGE);
            ViewBag.TypiconId = typiconEntity.Value.Id;

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

                return View(new TriodionRuleCreateEditModel());
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TriodionRuleCreateEditModel model)
        {
            var typiconEntity = QueryProcessor.Process(new TypiconEntityQuery(model.Id));

            model.Mode = TriodionRuleModelMode.Create;

            if (ModelState.IsValid
                && typiconEntity != null
                && IsAuthorizedToEdit(typiconEntity))
            {
                var command = new CreateTriodionRuleCommand(model.Id,
                    model.DayWorships.Select(c => (c.WorshipId, c.Order)),
                    model.TemplateId,
                    model.DaysFromEaster,
                    model.IsTransparent,
                    model.RuleDefinition,
                    model.ModRuleDefinition);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index), new { id = typiconEntity.Id });
            }

            ViewBag.Signs = QueryProcessor.GetSigns(typiconEntity.Id, DEFAULT_LANGUAGE);

            return View(model);
        }

        [Route("[controller]/[action]/{daysFromEaster?}")]
        public IActionResult GetDayWorships(int? daysFromEaster)
        {
            if (!daysFromEaster.HasValue)
            {
                daysFromEaster = 0;
            }

            var result = QueryProcessor.Process(new TriodionDayWorshipQuery(daysFromEaster.Value, DEFAULT_LANGUAGE));

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

        protected override Expression<Func<TriodionRuleGridModel, bool>> BuildExpression(string searchValue)
        {
            return m => m.Name == searchValue
                    || m.TemplateName == searchValue
                    || m.DaysFromEaster.ToString() == searchValue;
        }

        protected override IGridQuery<TriodionRuleGridModel> GetQuery(int id) => new AllTriodionRulesWebQuery(id, DEFAULT_LANGUAGE);

        protected override TypiconEntityByChildQuery<TriodionRule> GetTypiconEntityByChildQuery(int id)
            => new TypiconEntityByTriodionRuleQuery(id);

        protected override DeleteRuleCommandBase<TriodionRule> GetDeleteCommand(int id)
            => new DeleteTriodionRuleCommand(id);

        #endregion
    }
}