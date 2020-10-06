using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Interfaces;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Web.Extensions;
using TypiconOnline.WebServices.Authorization;

namespace TypiconOnline.Web.Controllers
{
    [Authorize(Roles = RoleConstants.AdminAndEditorRoles)]
    public abstract class TypiconBaseController<T>: GridBaseController<T> where T : IGridModel
    {
        public TypiconBaseController(
            IQueryProcessor queryProcessor,
            IAuthorizationService authorizationService,
            ICommandProcessor commandProcessor) : base(queryProcessor, commandProcessor)
        {
            AuthorizationService = authorizationService;
        }

        protected IAuthorizationService AuthorizationService { get; }
        

        protected bool IsAuthorizedToEdit(int id)
        {
            var result = AuthorizationService.AuthorizeAsync(
                                                       User, new TypiconEntityCanEditKey(id),
                                                       new DefaultAuthorizationRequirement());
            return result.Result.Succeeded;
        }

        protected bool IsAuthorizedToEdit(TypiconEntity typiconEntity)
        {
            var result = AuthorizationService.AuthorizeAsync(
                                                       User, typiconEntity,
                                                       TypiconOperations.Edit);
            return result.Result.Succeeded;
        }

        protected bool IsTypiconsAuthor(int id)
        {
            var request = QueryProcessor.Process(new TypiconEntityQuery(id));

            var result = AuthorizationService.AuthorizeAsync(
                                                       User, request,
                                                       TypiconOperations.Delete);
            return result.Result.Succeeded;
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="id">Id Устава</param>
        /// <returns></returns>
        protected IActionResult LoadGridData(IGridQuery<T> query, int? id = null)
        {
            try
            {
                if (id.HasValue && !IsAuthorizedToEdit(id.Value))
                {
                    return Unauthorized();
                }

                return base.LoadGridData(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
