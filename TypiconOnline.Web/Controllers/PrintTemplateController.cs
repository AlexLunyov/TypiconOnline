using System;
using System.Collections.Generic;
using System.IO;
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
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Web.Extensions;
using TypiconOnline.Web.Models.TypiconViewModels;
using TypiconOnline.WebServices.Authorization;

namespace TypiconOnline.Web.Controllers
{
    //[Authorize(Roles = RoleConstants.AdminAndEditorRoles)]
    public class PrintTemplateController : TypiconChildBaseController<PrintDayTemplateGridModel, PrintDayTemplate>
    {
        private const string DEFAULT_LANGUAGE = "cs-ru";

        public PrintTemplateController(
            IQueryProcessor queryProcessor,
            IAuthorizationService authorizationService,
            ICommandProcessor commandProcessor) : base(queryProcessor, authorizationService, commandProcessor)
        {
        }

        public override IActionResult Index(int id)
        {
            if (IsAuthorizedToEdit(id))
            {
                ClearStoredData(GetQuery(id));

                var found = QueryProcessor.Process(new PrintWeekTemplateModelQuery(id));

                return View(found.Value);
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Возвращает Partial view для отображения и редактирования печтного шаблона седмицы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult WeekView(int id)
        {
            if (IsAuthorizedToEdit(id))
            {
                var found = QueryProcessor.Process(new PrintWeekTemplateModelQuery(id));

                return PartialView("_WeekView", found.Value);
            }

            return Unauthorized();
        }

        [HttpGet]
        public IActionResult CreateWeek(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var typiconEntity = QueryProcessor.Process(new TypiconEntityQuery(id));

            if (typiconEntity != null
                && IsAuthorizedToEdit(typiconEntity))
            {
                return View();
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWeek(PrintWeekTemplateCreateModel model)
        {
            var typiconEntity = QueryProcessor.Process(new TypiconEntityQuery(model.Id));

            if (ModelState.IsValid
                && typiconEntity != null
                && IsAuthorizedToEdit(typiconEntity))
            {
                byte[] data = null;
                string fileName = null;

                //if (model.File != null)
                //{
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(model.File.OpenReadStream()))
                {
                    data = binaryReader.ReadBytes((int)model.File.Length);
                }

                fileName = model.File.FileName;
                //}

                var command = new CreatePrintWeekTemplateCommand(id: model.Id
                    , daysPerPage: model.DaysPerPage
                    , file: data
                    , fileName: fileName);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index), new { id = typiconEntity.Id });
            }

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">PrintWeekTemplateId</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditWeek(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var typiconEntity = QueryProcessor.Process(new TypiconEntityByPrintWeekTemplateQuery(id));

            if (typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                var found = QueryProcessor.Process(new PrintWeekTemplateEditQuery(id));
                ViewBag.TypiconId = typiconEntity.Value.Id.ToString();

                if (found.Success)
                {
                    return View(found.Value);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditWeek(PrintWeekTemplateEditModel model)
        {
            var typiconEntity = QueryProcessor.Process(new TypiconEntityByPrintWeekTemplateQuery(model.Id));

            if (ModelState.IsValid
                && typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                byte[] data = null;
                string fileName = null;

                if (model.File != null)
                {
                    //считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(model.File.OpenReadStream()))
                    {
                        data = binaryReader.ReadBytes((int)model.File.Length);
                    }

                    fileName = model.File.FileName;
                }

                var command = new EditPrintWeekTemplateCommand(id: model.Id
                    , daysPerPage: model.DaysPerPage
                    , file: data
                    , fileName: fileName);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index), new { id = typiconEntity.Value.Id });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateDay(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var typiconEntity = QueryProcessor.Process(new TypiconEntityQuery(id));

            if (typiconEntity != null
                && IsAuthorizedToEdit(typiconEntity))
            {
                return View();
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDay(PrintDayTemplateCreateModel model)
        {
            var typiconEntity = QueryProcessor.Process(new TypiconEntityQuery(model.Id));

            if (ModelState.IsValid
                && typiconEntity != null
                && IsAuthorizedToEdit(typiconEntity))
            {
                byte[] data = null;
                string fileName = null;

                //if (model.File != null)
                //{
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(model.File.OpenReadStream()))
                    {
                        data = binaryReader.ReadBytes((int)model.File.Length);
                    }

                    fileName = model.File.FileName;
                //}

                var command = new CreatePrintDayTemplateCommand(id: model.Id
                    , number: model.Number
                    , name: model.Name
                    , icon: model.Icon
                    , isRed: model.IsRed
                    , file: data
                    , fileName: fileName
                    , isDefault: model.IsDefault);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index), new { id = typiconEntity.Id });
            }

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">PrintDayTemplateId</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditDay(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var typiconEntity = QueryProcessor.Process(new TypiconEntityByPrintDayTemplateQuery(id));

            if (typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                var found = QueryProcessor.Process(new PrintDayTemplateEditQuery(id));

                ViewBag.TypiconId = typiconEntity.Value.Id.ToString();

                if (found.Success)
                {
                    return View(found.Value);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditDay(PrintDayTemplateEditModel model)
        {
            var typiconEntity = QueryProcessor.Process(new TypiconEntityByPrintDayTemplateQuery(model.Id));

            if (ModelState.IsValid
                && typiconEntity.Success
                && IsAuthorizedToEdit(typiconEntity.Value))
            {
                byte[] data = null;
                string fileName = null;

                if (model.File != null)
                {
                    //считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(model.File.OpenReadStream()))
                    {
                        data = binaryReader.ReadBytes((int)model.File.Length);
                    }

                    fileName = model.File.FileName;
                }

                var command = new EditPrintDayTemplateCommand(id: model.Id
                    , number: model.Number
                    , name: model.Name
                    , icon: model.Icon
                    , isRed: model.IsRed
                    , file: data
                    , fileName: fileName
                    , isDefault:model.IsDefault);

                await CommandProcessor.ExecuteAsync(command);

                return RedirectToAction(nameof(Index), new { id = typiconEntity.Value.Id });
            }

            return View(model);
        }

        /// <summary>
        /// Скачивание Печатного шаблона дня
        /// </summary>
        /// <param name="id">PrintDayTemplateId</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DownloadDay(int id)
        {
            var dwnldResult = QueryProcessor.Process(new DownloadPrintDayTemplateQuery(id));

            if (dwnldResult.Success)
            {
                return File(fileContents: dwnldResult.Value.Content, contentType: dwnldResult.Value.ContentType, fileDownloadName: dwnldResult.Value.FileDownloadName );
            }
            else
            {
                return new JsonResult(new { error = dwnldResult.Error });
            }
        }

        /// <summary>
        /// Скачивание Печатного шаблона седмицы
        /// </summary>
        /// <param name="id">PrintDayTemplateId</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DownloadWeek(int id)
        {
            var dwnldResult = QueryProcessor.Process(new DownloadPrintWeekTemplateQuery(id));

            if (dwnldResult.Success)
            {
                return File(fileContents: dwnldResult.Value.Content, contentType: dwnldResult.Value.ContentType, fileDownloadName: dwnldResult.Value.FileDownloadName);
            }
            else
            {
                return new JsonResult(new { error = dwnldResult.Error });
            }
        }

        //[Route("[controller]/[action]/{date?}")]
        //public IActionResult GetDayWorships(DateTime? date)
        //{
        //    var result = QueryProcessor.Process(new MenologyDayWorshipQuery(date, DEFAULT_LANGUAGE));

        //    if (result.Success)
        //    {
        //        return Json(new { data = result.Value.ToList() });
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        #region Overrides

        protected override Expression<Func<PrintDayTemplateGridModel, bool>> BuildExpression(string searchValue)
        {
            return m => m.Name == searchValue
                    || m.Number.ToString() == searchValue;
        }

        protected override IGridQuery<PrintDayTemplateGridModel> GetQuery(int id) => new AllPrintDayTemplatesQuery(id);

        protected override TypiconEntityByChildQuery<PrintDayTemplate> GetTypiconEntityByChildQuery(int id)
            => new TypiconEntityByPrintDayTemplateQuery(id);

        protected override DeleteRuleCommandBase<PrintDayTemplate> GetDeleteCommand(int id)
            => new DeletePrintDayTemplateCommand(id);

        #endregion
    }
}
