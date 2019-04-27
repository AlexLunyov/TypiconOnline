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
            IDataQueryProcessor queryProcessor, 
            IAuthorizationService authorizationService, 
            ICommandProcessor commandProcessor) : base(queryProcessor, authorizationService, commandProcessor)
        {
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(int id)
        {
            if (await IsAuthorizedToEdit(id))
            {
                ClearStoredData(GetQuery(id));
                return View();
            }
            else
            {
                return Unauthorized();
            }
        }

        public async Task<IActionResult> LoadData(int id)
        {
            return await LoadGridData(GetQuery(id), id);
        }

        protected abstract IGridQuery<T> GetQuery(int id);
    }
}
