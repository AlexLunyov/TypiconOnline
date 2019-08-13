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
    public class ExplicitAddRuleController : TypiconChildBaseController<ExplicitAddRuleModel, ExplicitAddRule>
    {
        private const string DEFAULT_LANGUAGE = "cs-ru";
        //private readonly IDataQueryProcessor _queryProcessor;
        //private readonly IAuthorizationService _authorizationService;


        public ExplicitAddRuleController(
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

            var typiconEntity = QueryProcessor.Process(GetTypiconEntityByChildQuery(id));

            if (typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                ViewBag.TypiconId = typiconEntity.Value.Id;

                var found = QueryProcessor.Process(new ExplicitAddRuleEditQuery(id));

                if (found.Success)
                {
                    return View(found.Value);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExplicitAddRuleEditModel model)
        {
            var typiconEntity = QueryProcessor.Process(GetTypiconEntityByChildQuery(model.Id));

            if (ModelState.IsValid
                && typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                var command = new EditExplicitAddRuleCommand(model.Id,
                    model.Date,
                    model.RuleDefinition);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index), new { id = typiconEntity.Value.Id });
            }

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
                return View(new ExplicitAddRuleEditModel());
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExplicitAddRuleEditModel model)
        {
            var typiconEntity = QueryProcessor.Process(new TypiconEntityQuery(model.Id));

            if (ModelState.IsValid
                && typiconEntity != null
                && IsAuthorizedToEdit(typiconEntity))
            {
                var command = new CreateExplicitAddRuleCommand(model.Id,
                    model.Date,
                    model.RuleDefinition);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index), new { id = typiconEntity.Id });
            }

            return View(model);
        }

        #region Overrides

        protected override Expression<Func<ExplicitAddRuleModel, bool>> BuildExpression(string searchValue)
        {
            return m => m.Date == searchValue;
        }

        protected override IGridQuery<ExplicitAddRuleModel> GetQuery(int id) => new AllExplicitAddRulesQuery(id);

        protected override TypiconEntityByChildQuery<ExplicitAddRule> GetTypiconEntityByChildQuery(int id) 
            => new TypiconEntityByExplicitAddRuleQuery(id);

        protected override DeleteRuleCommandBase<ExplicitAddRule> GetDeleteCommand(int id)
            =>  new DeleteExplicitAddRuleCommand(id);

        #endregion
    }
}
