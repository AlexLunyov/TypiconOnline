using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Web.Controllers
{
    public abstract class TypiconChildBaseController<T> : TypiconBaseController<T> where T: IGridModel
    {
        public TypiconChildBaseController(
            IQueryProcessor queryProcessor, 
            IAuthorizationService authorizationService, 
            ICommandProcessor commandProcessor) : base(queryProcessor, authorizationService, commandProcessor)
        {
        }

        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            if (IsAuthorizedToEdit(id))
            {
                ClearStoredData(GetQuery(id));
                return View();
            }
            else
            {
                return Unauthorized();
            }
        }

        public IActionResult LoadData(int id)
        {
            return LoadGridData(GetQuery(id), id);
        }

        protected abstract IGridQuery<T> GetQuery(int id);
    }
}
