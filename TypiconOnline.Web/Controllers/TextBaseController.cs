using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Web.Controllers
{
    public abstract class TextBaseController<TGridModel> : GridBaseController<TGridModel> where TGridModel : IGridModel
    {
        protected const string DEFAULT_LANGUAGE = "cs-ru";

        public TextBaseController(IQueryProcessor queryProcessor
            , ICommandProcessor commandProcessor) 
            : base(queryProcessor, commandProcessor)
        {
        }

        // GET: <controller>
        public ActionResult Index()
        {
            ClearStoredData(GetQuery());
            return View();
        }

        // GET: MenologyDay/LoadData
        public IActionResult LoadData()
        {
            return LoadGridData(GetQuery());
        }

        protected abstract IGridQuery<TGridModel> GetQuery();
    }
}
