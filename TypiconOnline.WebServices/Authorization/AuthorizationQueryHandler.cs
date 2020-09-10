using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Claims;
using System.Text;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.WebServices.Authorization
{
    /// <summary>
    /// Обработчик запросов на права доступа
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class AuthorizationQueryHandler<TQuery, TResult> : QueryDecoratorBase<TQuery, TResult> where TQuery : IQuery<TResult>, IHasAuthorizedAccess
    {
        public AuthorizationQueryHandler(IQueryHandler<TQuery, TResult> decorated
            , IAuthorizationService authorizationService
            , IHttpContextAccessor httpContextAccessor) : base(decorated)
        {
            AuthorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));

            HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        private IAuthorizationService AuthorizationService { get; }

        private IHttpContextAccessor HttpContextAccessor { get; }

        private ClaimsPrincipal User => HttpContextAccessor.HttpContext?.User;

        public override TResult Handle(TQuery query)
        {
            if (!CheckPermissions(query))
            {
                throw new SecurityException();
            }

            return Decorated.Handle(query);
        }

        protected bool CheckPermissions(TQuery query)
        {
            //Если нет контекста - отказываем в доступе. Пока так
            if (User == null)
            {
                return false;
            }

            var result = AuthorizationService.AuthorizeAsync(
                                                       User, query.Key,
                                                       new DefaultAuthorizationRequirement());

            return result.Result.Succeeded; 
        }
    }
}
