using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Infrastructure.Common.Command;
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
    public class AuthorizationCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand, IHasAuthorizedAccess
    {
        

        public AuthorizationCommandHandler(ICommandHandler<TCommand> decorated
            , IAuthorizationService authorizationService
            , IHttpContextAccessor httpContextAccessor)
        {
            Decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));

            AuthorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));

            HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        private ICommandHandler<TCommand> Decorated { get; }

        private IAuthorizationService AuthorizationService { get; }

        private IHttpContextAccessor HttpContextAccessor { get; }

        private ClaimsPrincipal User => HttpContextAccessor.HttpContext?.User;

        public Task<Result> ExecuteAsync(TCommand command)
        {
            if (!CheckPermissions(command))
            {
                return Task.FromResult(Result.Fail(403, "Доступ запрещен"));
            }

            return Decorated.ExecuteAsync(command);
        }

        protected bool CheckPermissions(TCommand command)
        {
            //Если нет контекста - отказываем в доступе. Пока так
            if (User == null)
            {
                return false;
            }

            var result = AuthorizationService.AuthorizeAsync(
                                                       User, command.Key,
                                                       new DefaultAuthorizationRequirement());

            return result.Result.Succeeded; 
        }
    }
}
