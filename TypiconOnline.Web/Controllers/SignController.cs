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
using TypiconOnline.Web.Services;
using TypiconOnline.WebServices.Authorization;

namespace TypiconOnline.Web.Controllers
{
    //[Authorize(Roles = RoleConstants.AdminAndEditorRoles)]
    public class SignController : TypiconChildBaseController<SignGridModel, Sign>
    {
        private const string DEFAULT_LANGUAGE = "cs-ru";

        public SignController(
            IQueryProcessor queryProcessor,
            ICommandProcessor commandProcessor,
            IAuthorizationService authorizationService) : base(queryProcessor, authorizationService, commandProcessor)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">SignId</param>
        /// <param name="fromSchedule">Признак того, что пришли из графика богослужений</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id, bool fromSchedule = false)
            => Perform(() => QueryProcessor.Process(new SignEditQuery(id)),
                        sign =>
                        {
                            ViewBag.Signs = QueryProcessor.GetSigns(sign.Value.TypiconId, DEFAULT_LANGUAGE, id);
                            ViewBag.PrintTemplates = QueryProcessor.GetPrintTemplates(sign.Value.TypiconId);

                            ViewBag.IsFromSchedule = fromSchedule;

                            return View(sign.Value);
                        });

        [HttpPost]
        public IActionResult Edit(SignCreateEditModel model)
            => Perform(() => CommandProcessor
                                    .Execute(new EditSignCommand(model.Id,
                                        model.Name,
                                        model.TemplateId,
                                        model.IsAddition,
                                        model.PrintTemplateId,
                                        model.Priority,
                                        model.RuleDefinition,
                                        model.ModRuleDefinition)),
                       () =>
                       {
                           return (ViewBag.IsFromSchedule is bool b == true)
                                ? RedirectToAction(nameof(ScheduleSettingsController.Index), "ScheduleSettings", new { id = model.TypiconId })
                                : RedirectToAction(nameof(Index), new { id = model.TypiconId });
                       });

        [HttpGet]
        public IActionResult Create(int id)
        {
            if (IsAuthorizedToEdit(id))
            {
                ViewBag.Signs = QueryProcessor.GetSigns(id, DEFAULT_LANGUAGE);
                ViewBag.PrintTemplates = QueryProcessor.GetPrintTemplates(id);

                return View(new SignCreateEditModel());
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SignCreateEditModel model)
        {
            if (ModelState.IsValid
                && IsAuthorizedToEdit(model.Id))
            {
                var command = new CreateSignCommand(model.Id,
                    model.Name,
                    model.TemplateId,
                    model.IsAddition,
                    model.PrintTemplateId,
                    model.Priority,
                    model.RuleDefinition,
                    model.ModRuleDefinition);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index), new { id = model.Id });
            }

            ViewBag.Signs = QueryProcessor.GetSigns(model.Id, DEFAULT_LANGUAGE, model.Id);
            ViewBag.PrintTemplates = QueryProcessor.GetPrintTemplates(model.Id);

            return View(model);
        }

        #region Overrides
        protected override IGridQuery<SignGridModel> GetQuery(int id) => new AllSignsQuery(id, DEFAULT_LANGUAGE);

        protected override TypiconEntityByChildQuery<Sign> GetTypiconEntityByChildQuery(int id) 
            => new TypiconEntityBySignQuery(id);

        protected override DeleteRuleCommandBase<Sign> GetDeleteCommand(int id)
            =>  new DeleteSignCommand(id);

        #endregion
    }
}
