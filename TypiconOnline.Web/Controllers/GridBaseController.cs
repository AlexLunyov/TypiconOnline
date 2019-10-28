using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Web.Extensions;

namespace TypiconOnline.Web.Controllers
{
    public abstract class GridBaseController<T> : Controller where T : IGridModel
    {
        public GridBaseController(
            IQueryProcessor queryProcessor,
            ICommandProcessor commandProcessor)
        {
            QueryProcessor = queryProcessor;
            CommandProcessor = commandProcessor;
        }

        protected IQueryProcessor QueryProcessor { get; }
        protected ICommandProcessor CommandProcessor { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected IActionResult LoadGridData(IGridQuery<T> query)
        {
            ////try
            ////{
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
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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
                    var col = request.Value.ToList();
                    HttpContext.Session.Set(query.GetKey(), col);

                    return Result.Ok(col.AsQueryable());
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
