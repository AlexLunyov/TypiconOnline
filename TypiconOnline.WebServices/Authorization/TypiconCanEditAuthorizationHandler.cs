using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.WebServices.Authorization
{
    public class TypiconCanEditAuthorizationHandler 
        : AuthorizationHandler<OperationAuthorizationRequirement, TypiconEntity>
    {
        UserManager<User> _userManager;

        public TypiconCanEditAuthorizationHandler(UserManager<User>
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   TypiconEntity resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.FromResult(0);
            }

            //нельзя редактировать, если Устав находится в каком-то процессе
            if (resource.Status == TypiconStatus.Approving
                || resource.Status == TypiconStatus.Publishing
                || resource.Status == TypiconStatus.Validating)
            {
                return Task.FromResult(0);
            }

            // Administrators can do anything.
            if (context.User.IsInRole(RoleConstants.AdministratorsRole))
            {
                context.Succeed(requirement);
            }

            // If we're not asking for create or delete permission, return.

            if (requirement.Name != AuthorizationConstants.CreateTypiconName &&
                requirement.Name != AuthorizationConstants.DeleteTypiconName)
            {
                return Task.FromResult(0);
            }

            var id = _userManager.GetUserId(context.User);

            if (resource.OwnerId.ToString() == id 
                || resource.EditableUserTypicons.Any(c => c.UserId.ToString() == id))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
