using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Web.Controllers
{
    [Authorize(Roles = RoleConstants.AdministratorsRole)]
    public class AdminController : Controller
    {
        private readonly IBooksExportManager _booksExportManager;

        public AdminController(IBooksExportManager booksExportManager)
        {
            _booksExportManager = booksExportManager ?? throw new ArgumentNullException(nameof(booksExportManager));
        }

        // GET admin
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return Get(DateTime.Now);
        //}

        [HttpGet]
        public ActionResult Operations()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ExportBooks()
        {

            var exportResult = _booksExportManager.Export();

            if (exportResult.Success)
            {
                return Json(new { file = exportResult.Value.Content, type = exportResult.Value.ContentType, filename = exportResult.Value.FileDownloadName });
            }
            else
            {
                return View(nameof(Operations), exportResult.Error);
            }

            
        }
    }
}
