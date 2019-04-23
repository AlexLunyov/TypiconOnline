using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Domain.WebQuery.Typicon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TypiconOnline.Domain.Identity;
using JqueryDataTables.ServerSide.AspNetCoreWeb;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Web.Extensions;
using TypiconOnline.Web.Models.TypiconViewModels;
using TypiconOnline.Domain.Command.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using System.Linq.Expressions;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TypiconOnline.Web.Controllers
{
    [Authorize(Roles = RoleConstants.AdminAndEditorRoles)]
    public class TypiconsController : TypiconBaseController<TypiconEntityFilteredModel>
    {
        private const string DEFAULT_LANGUAGE = "cs-ru";
        private readonly UserManager<User> _userManager;
        private readonly IJobRepository _jobs;

        public TypiconsController(
            ICommandProcessor commandProcessor,
            UserManager<User> userManager,
            IJobRepository jobs,
            IDataQueryProcessor queryProcessor,
            IAuthorizationService authorizationService
            ) : base(queryProcessor, authorizationService, commandProcessor)
        {
            _userManager = userManager;
            _jobs = jobs;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Typicons = QueryProcessor.GetTypicons();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTypiconModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new ApplicationException($"Невозможно найти Пользователя с Id = '{_userManager.GetUserId(User)}'.");
                }

                var command = new CreateTypiconCommand(model.Name, model.DefaultLanguage, model.TemplateId, user.Id);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        //[Route("{id?}")]
        public IActionResult Approve(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            _jobs.Create(new ApproveTypiconEntityJob(id));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LoadData()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                return await LoadGridData(new AllTypiconsFilteredQuery(user.Id, DEFAULT_LANGUAGE));
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var typicon = QueryProcessor.Process(new TypiconEditQuery(id));

            if (typicon.Failure)
            {
                return NotFound();
            }
            else
            {
                return View(typicon.Value);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TypiconEntityEditModel model)
        {
            if (ModelState.IsValid)
            {
                //проверить на права
                if (! await IsAuthorizedToEdit(model.Id))
                {
                    return NotFound();
                }

                var command = new EditTypiconCommand(model.Id, model.Name, model.DefaultLanguage);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("ShowGrid", "DemoGrid");
                }

                int result = 0;

                if (result > 0)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override Expression<Func<TypiconEntityFilteredModel, bool>> BuildExpression(string searchValue)
        {
            return m => m.Name == searchValue;
        }
    }
}
