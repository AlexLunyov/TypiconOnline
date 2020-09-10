using Microsoft.AspNetCore.Authorization;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypiconOnline.Web.Services
{
    /// <summary>
    /// Аdapter that can forward the call to components that are registered in Simple Injector
    /// 
    /// Взято отсюда: https://github.com/simpleinjector/SimpleInjector/issues/297#issuecomment-247963803
    /// </summary>
    public sealed class SimpleInjectorAuthorizationHandler : IAuthorizationHandler
    {
        private readonly Container container;
        public SimpleInjectorAuthorizationHandler(Container container) { this.container = container; }

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (var handler in container.GetAllInstances<IAuthorizationHandler>())
            {
                await handler.HandleAsync(context);
            }
        }
    }
}
