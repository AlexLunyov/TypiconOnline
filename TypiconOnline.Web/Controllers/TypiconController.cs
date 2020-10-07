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
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;
using TypiconOnline.Domain.ItemTypes;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TypiconOnline.Web.Controllers
{
    [Authorize(Roles = RoleConstants.AdminAndEditorRoles)]
    public class TypiconController : TypiconBaseController<TypiconEntityFilteredModel>
    {
        private const string DEFAULT_LANGUAGE = "cs-ru";
        private readonly UserManager<User> _userManager;
        private readonly IJobRepository _jobs;
        private readonly ITypiconExportManager _exportManager;
        private readonly ITypiconImportManager _importManager;

        public TypiconController(
            ICommandProcessor commandProcessor
            , UserManager<User> userManager
            , IJobRepository jobs
            , IQueryProcessor queryProcessor
            , IAuthorizationService authorizationService
            , ITypiconExportManager exportManager
            , ITypiconImportManager importManager
            ) : base(queryProcessor, authorizationService, commandProcessor)
        {
            _userManager = userManager;
            _jobs = jobs;
            _exportManager = exportManager;
            _importManager = importManager;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            ClearStoredData(new AllTypiconsFilteredQuery(user.Id, DEFAULT_LANGUAGE));

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Typicons = QueryProcessor.GetPublicTypicons(withTemplates: true);

            return View(new CreateTypiconModel());
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

                var command = new CreateTypiconCommand(model.Name, model.Description, model.SystemName, model.DefaultLanguage, model.TemplateId, user.Id);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Typicons = QueryProcessor.GetPublicTypicons(withTemplates: true);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.AdministratorsRole)]
        public IActionResult Review(int id)
        {
            var model = QueryProcessor.Process(new TypiconClaimQuery(id));

            if (model.Failure)
            {
                return NotFound();
            }
            else
            {
                return View(model.Value);
            }
        }

        //[HttpPost]
        [Authorize(Roles = RoleConstants.AdministratorsRole)]
        //[ValidateAntiForgeryToken]
        //[Route("{id?}")]
        public IActionResult Approve(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            _jobs.Create(new ApproveTypiconClaimJob(id));

            return RedirectToAction("Index");
        }

        //[HttpPost]
        [Authorize(Roles = RoleConstants.AdministratorsRole)]
        //[ValidateAntiForgeryToken]
        //[Route("{id?}")]
        public async Task<IActionResult> Reject(TypiconClaimModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new RejectTypiconClaimCommand(model.Id, model.ResultMessage);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction("Index");
            }

            return View(nameof(Review), model);
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

                return LoadGridData(new AllTypiconsFilteredQuery(user.Id, DEFAULT_LANGUAGE));
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
            var model = GetModel(id);

            if (model.Failure)
            {
                return NotFound();
            }
            else
            {
                return View(model.Value);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TypiconEntityEditModel model)
        {
            if (ModelState.IsValid)
            {
                //проверить на права
                if (!IsAuthorizedToEdit(model.Id))
                {
                    return NotFound();
                }

                var command = new EditTypiconCommand(model.Id, model.Name, model.Description, model.IsTemplate, model.DefaultLanguage);

                await CommandProcessor.ExecuteAsync(command);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsTypiconsAuthor(id))
            {
                return Json(new { error = "Ошибка авторизации."});
            }

            var command = new DeleteTypiconCommand(id);

            var result = await CommandProcessor.ExecuteAsync(command);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Удаление Заявки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            var command = new DeleteTypiconClaimCommand(HttpContext.User, id);

            var result = await CommandProcessor.ExecuteAsync(command);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Экспорт сериализованного Устава
        /// </summary>
        /// <param name="typiconVersionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = RoleConstants.AdministratorsRole)]
        public IActionResult ExportGet(int id)
        {
            var versions = QueryProcessor.Process(new TypiconVersionsQuery(id));

            if (versions.Success)
            {
                return PartialView("_ExportPartial", versions.Value.Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() }));
            }

            return NotFound();
        }

        /// <summary>
        /// Экспорт сериализованного Устава
        /// </summary>
        /// <param name="typiconVersionId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = RoleConstants.AdministratorsRole)]
        public IActionResult Export(int versionId)
        {
            var exportResult = _exportManager.Export(versionId);

            if (exportResult.Success)
            {
                return Json(new { file = exportResult.Value.Content, type = exportResult.Value.ContentType, filename = exportResult.Value.FileDownloadName });
            }
            else
            {
                return new JsonResult(new { error = exportResult.Error });
            }
        }

        /// <summary>
        /// Экспорт сериализованного Устава
        /// </summary>
        /// <param name="typiconVersionId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = RoleConstants.AdministratorsRole)]
        public IActionResult Import(IFormFile file)
        {
            if (file != null)
            {
                byte[] data = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    data = binaryReader.ReadBytes((int)file.Length);
                }

                var importResult = _importManager.Import(data);

                return new JsonResult(new { success = importResult.Success, msg = importResult.Error });
            }
            
            return new JsonResult(new { success = false, msg = "Ошибка: пустой файл" });
        }

        [HttpGet]
        public IActionResult Operations(int id) => Edit(id);

        [HttpGet]
        public IActionResult Editors(int id) => Edit(id);

        //[HttpPost]
        public IActionResult Publish(TypiconEntityEditModel model)
        {
            if (IsAuthorizedToEdit(model.Id))
            {
                _jobs.Create(new PublishTypiconJob(model.Id, model.DeleteModifiedOutputDays));
            }
            

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Поиск пользователей для добавления в Редакторы Устава
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IActionResult SearchUsers(string search)
        {
            var found = QueryProcessor.Process(new SearchUserQuery(search));

            return Json(found);
        }

        /// <summary>
        /// Добавляет Редактора Устава
        /// </summary>
        /// <param name="typiconId"></param>
        /// <param name="editorId"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddEditor(int typiconId, int editorId)
        {
            if (!IsAuthorizedToEdit(typiconId))
            {
                return new ChallengeResult();
            }

            var command = new AddEditorCommand(typiconId, editorId);

            await CommandProcessor.ExecuteAsync(command);

            return RedirectToAction(nameof(Edit), "Typicon", new { id = typiconId }, "panel_editors");
        }

        /// <summary>
        /// Удаляет Редактора Устава
        /// </summary>
        /// <param name="typiconId"></param>
        /// <param name="editorId"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteEditor(int typiconId, int editorId)
        {
            if (!IsAuthorizedToEdit(typiconId))
            {
                return new ChallengeResult();
            }

            var command = new DeleteEditorCommand(typiconId, editorId);

            await CommandProcessor.ExecuteAsync(command);

            return RedirectToAction(nameof(Edit), "Typicon", new { id = typiconId }, "panel_editors");
        }

        private Result<TypiconEntityEditModel> GetModel(int typiconId)
        {
            //проверить на права
            if (!IsAuthorizedToEdit(typiconId))
            {
                return Result.Fail<TypiconEntityEditModel>($"Вы не имеете прав на просмотр Устава с Id={typiconId}");
            }

            return QueryProcessor.Process(new TypiconEditQuery(typiconId));
        }

        [AcceptVerbs("GET", "POST")]
        //[Route("[controller]/[action]/{systemName}")]
        public IActionResult IsNameUnique(string systemName)
        {
            var isUnique = QueryProcessor.Process(new IsNameUniqueQuery(systemName));
            if (!isUnique.Success)
            {
                return Json($"Наименование '{systemName}' уже занято.");
            }

            return Json(true);
        }
    }
}
