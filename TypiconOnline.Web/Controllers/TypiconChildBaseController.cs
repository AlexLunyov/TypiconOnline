using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypiconOnline.Domain.Command.Typicon;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Web.Controllers
{
    public abstract class TypiconChildBaseController<TGridModel, TDomain> : TypiconBaseController<TGridModel> 
        where TGridModel : IGridModel
        where TDomain : RuleEntity, new()
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

        [HttpPost]
        [Route("[controller]/[action]/{typiconId}/{ruleId}")]
        public async Task<IActionResult> Delete(int typiconId, int ruleId)
        {
            try
            {
                //проверка на права доступа

                var typiconEntity = QueryProcessor.Process(GetTypiconEntityByChildQuery(typiconId));

                if (typiconEntity.Success
                    && IsAuthorizedToEdit(typiconEntity.Value))
                {
                    //удаление
                    var result = await CommandProcessor.ExecuteAsync(GetDeleteCommand(ruleId));

                    ClearStoredData(GetQuery(typiconId));

                    return Json(data: result.Success);
                }

                return Json(data: typiconEntity.Success);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected abstract IGridQuery<TGridModel> GetQuery(int id);

        protected abstract TypiconEntityByChildQuery<TDomain> GetTypiconEntityByChildQuery(int id);
        protected abstract DeleteRuleCommandBase<TDomain> GetDeleteCommand(int id);
    }
}
