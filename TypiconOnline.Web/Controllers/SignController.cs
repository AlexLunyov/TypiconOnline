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
    public class SignController : TypiconChildBaseController<SignModel>
    {
        private const string DEFAULT_LANGUAGE = "cs-ru";
        //private readonly IDataQueryProcessor _queryProcessor;
        //private readonly IAuthorizationService _authorizationService;


        public SignController(
            IQueryProcessor queryProcessor,
            IAuthorizationService authorizationService,
            ICommandProcessor commandProcessor) : base(queryProcessor, authorizationService, commandProcessor)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">SignId</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var typiconEntity = QueryProcessor.Process(new TypiconEntityBySignQuery(id));

            if (typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                ViewBag.Signs = QueryProcessor.GetSigns(typiconEntity.Value.Id, DEFAULT_LANGUAGE, id);
                ViewBag.TypiconId = typiconEntity.Value.Id;

                var found = QueryProcessor.Process(new SignEditQuery(id));

                if (found.Success)
                {
                    return View(found.Value);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SignEditModel model)
        {
            var typiconEntity = QueryProcessor.Process(new TypiconEntityBySignQuery(model.Id));

            if (ModelState.IsValid
                && typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                var command = new EditSignCommand(model.Id,
                    model.Name,
                    model.TemplateId,
                    model.IsAddition,
                    model.Number,
                    model.Priority,
                    model.RuleDefinition,
                    model.ModRuleDefinition);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index), new { id = typiconEntity.Value.Id });
            }

            ViewBag.Signs = QueryProcessor.GetSigns(typiconEntity.Value.Id, DEFAULT_LANGUAGE, model.Id);
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

                return View(new SignEditModel());
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SignEditModel model)
        {
            var typiconEntity = QueryProcessor.Process(new TypiconEntityQuery(model.Id));

            if (ModelState.IsValid
                && typiconEntity != null
                && IsAuthorizedToEdit(typiconEntity))
            {
                var command = new CreateSignCommand(model.Id,
                    model.Name,
                    model.TemplateId,
                    model.IsAddition,
                    model.Number,
                    model.Priority,
                    model.RuleDefinition,
                    model.ModRuleDefinition);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index), new { id = typiconEntity.Id });
            }

            ViewBag.Signs = QueryProcessor.GetSigns(typiconEntity.Id, DEFAULT_LANGUAGE, model.Id);

            return View(model);
        }

        #region Overrides

        protected override Expression<Func<SignModel, bool>> BuildExpression(string searchValue)
        {
            return m => m.Name == searchValue || m.TemplateName == searchValue;
        }

        protected override IGridQuery<SignModel> GetQuery(int id) => new AllSignsQuery(id, DEFAULT_LANGUAGE);

        #endregion
    }
}
