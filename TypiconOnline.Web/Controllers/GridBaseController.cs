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
using TypiconOnline.Web.Services;

namespace TypiconOnline.Web.Controllers
{
    public abstract class GridBaseController<T> : BaseController where T : IGridModel
    {
        private readonly GridStore<T> _gridStore;

        public GridBaseController(
            IQueryProcessor queryProcessor,
            ICommandProcessor commandProcessor)
            : base(queryProcessor, commandProcessor)
        {
            _gridStore = new GridStore<T>(queryProcessor
                , commandProcessor
                , this
                , BuildExpression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected IActionResult LoadGridData(IGridQuery<T> query) => _gridStore.LoadGridData(query);

        protected abstract Func<T, string, bool> BuildExpression { get; }

        protected void ClearStoredData(IGridQuery<T> query)
        {
            //if (HttpContext.Session.Keys.Contains(query.GetKey()))
            //{
            //    HttpContext.Session.Remove(query.GetKey());
            //}
        }
    }
}
