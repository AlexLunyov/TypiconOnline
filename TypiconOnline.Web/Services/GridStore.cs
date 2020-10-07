using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Web.Extensions;
using System.Linq.Dynamic;

namespace TypiconOnline.Web.Services
{
    /// <summary>
    /// Хранилище для элементов таблиц
    /// </summary>
    public class GridStore<T> where T : IGridModel
    {
        public GridStore(
            IQueryProcessor queryProcessor,
            ICommandProcessor commandProcessor,
            Controller controller,
            bool storeToSession = false)
        {
            QueryProcessor = queryProcessor;
            CommandProcessor = commandProcessor;
            Controller = controller;
            StoreToSession = storeToSession;
        }

        protected IQueryProcessor QueryProcessor { get; }
        protected ICommandProcessor CommandProcessor { get; }

        protected Controller Controller { get; }

        public bool StoreToSession { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IActionResult LoadGridData(IGridQuery<T> query)
        {
            ////try
            ////{
            var draw = Controller.HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count
            var start = Controller.Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20
            var length = Controller.Request.Form["length"].FirstOrDefault();
            // Sort Column Name
            var sortColumn = Controller.Request.Form["columns[" + Controller.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction ( asc ,desc)
            var sortColumnDirection = Controller.Request.Form["order[0][dir]"].FirstOrDefault();
            // Search Value from (Search box)
            var searchValue = Controller.Request.Form["search[value]"].FirstOrDefault();

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
                    signData = signData.WhereAny(query.Search($"%{searchValue}%"));
                }

                //Paging 
                var data = signData.Skip(skip).Take(pageSize).ToList();
                //total number of rows count 
                recordsTotal = signData.Count();
                //Returning Json Data
                return Controller.Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            else
            {
                return Controller.NotFound();
            }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private Result<IQueryable<T>> LoadStoredData(IGridQuery<T> query)
        {
            if (StoreToSession && Controller.HttpContext.Session.Keys.Contains(query.GetCacheKey()))
            {
                return Result.Ok(Controller.HttpContext.Session.Get<List<T>>(query.GetCacheKey()).AsQueryable());
            }
            else
            {
                var request = QueryProcessor.Process(query);
                if (request.Success)
                {
                    var col = request.Value.ToList();

                    if (StoreToSession)
                    {
                        //если сохраняем в сессию
                        Controller.HttpContext.Session.Set(query.GetCacheKey(), col);
                    }
                }

                return request;
            }
        }

        public void ClearStoredData(IGridQuery<T> query)
        {
            if (Controller.HttpContext.Session.Keys.Contains(query.GetCacheKey()))
            {
                Controller.HttpContext.Session.Remove(query.GetCacheKey());
            }
        }
    }
}
