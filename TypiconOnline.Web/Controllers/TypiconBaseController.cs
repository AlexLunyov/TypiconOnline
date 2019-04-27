using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Web.Extensions;
using TypiconOnline.WebServices.Authorization;

namespace TypiconOnline.Web.Controllers
{
    [Authorize(Roles = RoleConstants.AdminAndEditorRoles)]
    public abstract class TypiconBaseController<T>: Controller where T : IGridModel
    {
        public TypiconBaseController(
            IDataQueryProcessor queryProcessor,
            IAuthorizationService authorizationService,
            ICommandProcessor commandProcessor)
        {
            QueryProcessor = queryProcessor;
            AuthorizationService = authorizationService;
            CommandProcessor = commandProcessor;
        }

        protected IDataQueryProcessor QueryProcessor { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected ICommandProcessor CommandProcessor { get; }

        

        protected async Task<bool> IsAuthorizedToEdit(int id)
        {
            var request = QueryProcessor.Process(new TypiconEntityQuery(id));

            var result = await AuthorizationService.AuthorizeAsync(
                                                       User, request,
                                                       TypiconOperations.Edit);
            return result.Succeeded;
        }

        protected async Task<bool> IsAuthorizedToEdit(TypiconEntity typiconEntity)
        {
            var result = await AuthorizationService.AuthorizeAsync(
                                                       User, typiconEntity,
                                                       TypiconOperations.Edit);
            return result.Succeeded;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="id">Id Устава</param>
        /// <returns></returns>
        protected async Task<IActionResult> LoadGridData(IGridQuery<T> query, int? id = null)
        {
            try
            {
                if (id.HasValue && !await IsAuthorizedToEdit(id.Value))
                {
                    return Unauthorized();
                }

                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Sign data
                var request = LoadStoredData(query);

                if (request.Success)
                {
                    var signData = request.Value;

                    //Sorting
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                    {
                        signData = signData.OrderBy(sortColumn + " " + sortColumnDirection);
                    }
                    //Search
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        signData = signData.Where(BuildExpression(searchValue));
                    }

                    //Paging 
                    var data = signData.Skip(skip).Take(pageSize).ToList();
                    //total number of rows count 
                    recordsTotal = signData.Count();
                    //Returning Json Data
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected abstract Expression<Func<T, bool>> BuildExpression(string searchValue);

        private Result<IQueryable<T>> LoadStoredData(IGridQuery<T> query)
        {
            if (HttpContext.Session.Keys.Contains(query.GetKey()))
            {
                return Result.Ok(HttpContext.Session.Get<List<T>>(query.GetKey()).AsQueryable());
            }
            else
            {
                var request = QueryProcessor.Process(query);
                if (request.Success)
                {
                    HttpContext.Session.Set(query.GetKey(), request.Value.ToList());
                }

                return request;
            }
        }

        protected void ClearStoredData(IGridQuery<T> query)
        {
            if (HttpContext.Session.Keys.Contains(query.GetKey()))
            {
                HttpContext.Session.Remove(query.GetKey());
            }
        }
    }
}
