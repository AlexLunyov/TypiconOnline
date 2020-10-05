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
using TypiconOnline.Web.Models;
using TypiconOnline.Web.Services;
using TypiconOnline.WebServices.Authorization;

namespace TypiconOnline.Web.Controllers
{
    //[Authorize(Roles = RoleConstants.AdminAndEditorRoles)]
    public class TypiconVariablesController : BaseController
    {
        private const string DEFAULT_LANGUAGE = "cs-ru";
        //private readonly IDataQueryProcessor _queryProcessor;
        //private readonly IAuthorizationService _authorizationService;

        private readonly GridStore<TypiconVariableModel> variableStore;

        public TypiconVariablesController(
            IQueryProcessor queryProcessor,
            ICommandProcessor commandProcessor) : base(queryProcessor, commandProcessor)
        {
            variableStore = new GridStore<TypiconVariableModel>(queryProcessor
                , commandProcessor
                , this
                , (m, s) =>
                {
                    return m.Name == s
                    || m.Type.ToString() == s
                    || m.Count.ToString() == s;
                });
        }

        public IActionResult Index(int id)
        {
            return View();
        }

        public IActionResult LoadData(int id) => variableStore.LoadGridData(new AllVariablesQuery(id));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">SignId</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = QueryProcessor.Process(new VariableEditQuery(id));

            if (result.Success)
            {
                switch (result.Value.Type)
                {
                    case VariableType.Time:
                        {
                            return View("EditTime", result.Value);
                        }
                    case VariableType.Worship:
                        {
                            return View("EditWorship", result.Value);
                        }
                    default:
                        {
                            //TODO: по умолчанию долно быть другое значение
                            return View("EditTime", result.Value);
                        }
                }
            }
            else
            {
                return Fail(result.ErrorCode);
            }
        }

        [HttpPost]
        public IActionResult EditTime(VariableEditTimeModel model)
            => Perform(() => CommandProcessor.Execute(new EditTypiconTimeVariableCommand
                                 (model.Id
                                 , model.Description
                                 , model.Value?.ToString() ?? string.Empty)),
                       () => RedirectToAction(nameof(Index), new { id = model.TypiconId }));

        [HttpPost]
        public IActionResult EditWorship(VariableEditWorshipModel model)
            => Perform(() => CommandProcessor.Execute(new EditTypiconWorshipVariableCommand
                                 (model.Id
                                 , model.Description
                                 , model.Value)),
                       () => RedirectToAction(nameof(Index), new { id = model.TypiconId }));

        //[HttpPost]
        //public async Task<IActionResult> Edit(VariableEditModel model)
        //{
        //    try
        //    {
        //        var typiconEntity = QueryProcessor.Process(new TypiconEntityByVariableQuery(model.Id));

        //        if (ModelState.IsValid
        //            && typiconEntity.Success
        //            && IsAuthorizedToEdit(typiconEntity.Value))
        //        {
        //            var command = new EditTypiconTimeVariableCommand(model.Id, model.Description);

        //            var result = await CommandProcessor.ExecuteAsync(command);

        //            if (result.Success)
        //            {
        //                ClearStoredData(new AllVariablesQuery(typiconEntity.Value.Id));
        //            }

        //            return Json(data: result.Error);
        //        }

        //        return Json(data: typiconEntity.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpGet]
        //public IActionResult Define(int id)
        //{
        //    if (id < 1)
        //    {
        //        return NotFound();
        //    }

        //    var typiconEntity = QueryProcessor.Process(new TypiconEntityByVariableQuery(id));

        //    if (typiconEntity.Success
        //        && IsAuthorizedToEdit(typiconEntity.Value))
        //    {
        //        ViewBag.TypiconId = typiconEntity.Value.Id;

        //        var found = QueryProcessor.Process(new VariableEditQuery(id));

        //        if (found.Success)
        //        {
        //            return PartialView("_DefinePartial", new VariableDefineModel(found.Value));
        //        }
        //    }

        //    return NotFound();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Define(VariableDefineModel model)
        //{
        //    try
        //    {
        //        var typiconEntity = QueryProcessor.Process(new TypiconEntityByVariableQuery(model.Id));

        //        if (ModelState.IsValid
        //            && typiconEntity.Success
        //            && IsAuthorizedToEdit(typiconEntity.Value))
        //        {
        //            var command = new DefineTypiconVariableCommand(model.Id, model.Value);

        //            var result = await CommandProcessor.ExecuteAsync(command);

        //            if (result.Success)
        //            {
        //                ClearStoredData(new AllVariablesQuery(typiconEntity.Value.Id));
        //            }

        //            return Json(data: result.Error);
        //        }

        //        return Json(data: typiconEntity.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public IActionResult LoadData(int id)
        //{
        //    return LoadGridData(new AllVariablesQuery(id), id);
        //}
    }
}
