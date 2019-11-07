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
using TypiconOnline.Domain.Typicon.Variable;
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
    public class TypiconVariablesController : TypiconBaseController<TypiconVariableModel>
    {
        private const string DEFAULT_LANGUAGE = "cs-ru";
        //private readonly IDataQueryProcessor _queryProcessor;
        //private readonly IAuthorizationService _authorizationService;


        public TypiconVariablesController(
            IQueryProcessor queryProcessor,
            IAuthorizationService authorizationService,
            ICommandProcessor commandProcessor) : base(queryProcessor, authorizationService, commandProcessor)
        {
        }

        public IActionResult Index(int id)
        {
            ClearStoredData(new AllVariablesQuery(id));

            return View();
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

            var typiconEntity = QueryProcessor.Process(new TypiconEntityByVariableQuery(id));

            if (typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                ViewBag.TypiconId = typiconEntity.Value.Id;

                var found = QueryProcessor.Process(new VariableEditQuery(id));

                if (found.Success)
                {
                    return PartialView("_EditPartial", found.Value);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VariableEditModel model)
        {
            try
            {
                var typiconEntity = QueryProcessor.Process(new TypiconEntityByVariableQuery(model.Id));

                if (ModelState.IsValid
                    && typiconEntity.Success
                    && IsAuthorizedToEdit(typiconEntity.Value))
                {
                    var command = new EditTypiconVariableCommand(model.Id, model.Description);

                    var result = await CommandProcessor.ExecuteAsync(command);

                    if (result.Success)
                    {
                        ClearStoredData(new AllVariablesQuery(typiconEntity.Value.Id));
                    }

                    return Json(data: result.Error);
                }

                return Json(data: typiconEntity.Error);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult Define(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var typiconEntity = QueryProcessor.Process(new TypiconEntityByVariableQuery(id));

            if (typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                ViewBag.TypiconId = typiconEntity.Value.Id;

                var found = QueryProcessor.Process(new VariableEditQuery(id));

                if (found.Success)
                {
                    return PartialView("_DefinePartial", new VariableDefineModel(found.Value));
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Define(VariableDefineModel model)
        {
            try
            {
                var typiconEntity = QueryProcessor.Process(new TypiconEntityByVariableQuery(model.Id));

                if (ModelState.IsValid
                    && typiconEntity.Success
                    && IsAuthorizedToEdit(typiconEntity.Value))
                {
                    var command = new DefineTypiconVariableCommand(model.Id, model.Value);

                    var result = await CommandProcessor.ExecuteAsync(command);

                    if (result.Success)
                    {
                        ClearStoredData(new AllVariablesQuery(typiconEntity.Value.Id));
                    }

                    return Json(data: result.Error);
                }

                return Json(data: typiconEntity.Error);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult LoadData(int id)
        {
            return LoadGridData(new AllVariablesQuery(id), id);
        }

        #region Overrides

        protected override Expression<Func<TypiconVariableModel, bool>> BuildExpression(string searchValue)
        {
            return m => m.Name == searchValue || m.Type.ToString() == searchValue || m.Count.ToString() == searchValue;
        }

        #endregion
    }
}
