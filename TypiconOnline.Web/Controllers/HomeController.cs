using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Web.Models;

namespace TypiconOnline.Web.Controllers
{
    public class HomeController : Controller
    {
        IDataQueryProcessor queryProcessor;

        public HomeController(IDataQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor ?? throw new ArgumentNullException("queryProcessor in HomeController");
        }

        [DefaultBreadcrumb("Главная")]
        public IActionResult Index()
        {
            ViewBag.Entities = queryProcessor.Process(new AllTypiconsQuery());

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
