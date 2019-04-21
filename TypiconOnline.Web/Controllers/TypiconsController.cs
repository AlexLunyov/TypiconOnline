using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic;
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
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TypiconOnline.Web.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    public class TypiconsController : Controller
    {
        private const string DEFAULT_LANGUAGE = "cs-ru";
        private readonly IDataQueryProcessor _queryProcessor;
        private readonly ICommandProcessor _commandProcessor;
        private readonly UserManager<User> _userManager;
        private readonly IJobRepository _jobs;

        public TypiconsController(
            IDataQueryProcessor queryProcessor,
            ICommandProcessor commandProcessor,
            UserManager<User> userManager,
            IJobRepository jobs
            )
        {
            _queryProcessor = queryProcessor;
            _commandProcessor = commandProcessor;
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
            ViewBag.Typicons = _queryProcessor.GetTypicons();

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

                await _commandProcessor.ExecuteAsync(command);

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

        public async Task<IActionResult> LoadData([FromBody]DTParameters param)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var data = _queryProcessor.Process(new AllTypiconsFilteredQuery(user.Id, DEFAULT_LANGUAGE));

                return new JsonResult(new DTResult<TypiconEntityFilteredModel>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = data.Count(),
                    recordsTotal = data.Count()
                });
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

            var typicon = _queryProcessor.Process(new TypiconEditQuery(id));

            if (typicon == null)
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
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new ApplicationException($"Невозможно найти Пользователя с Id = '{_userManager.GetUserId(User)}'.");
                }

                //проверить на права

                var command = new EditTypiconCommand(model.Id, model.Name, model.DefaultLanguage);

                await _commandProcessor.ExecuteAsync(command);

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


    }
}
