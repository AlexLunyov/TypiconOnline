using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Output;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.WebServices.Authorization
{
    public class OutputWorshipCanEditAuthorization : AuthorizationBase<OutputWorshipCanEditKey>
    {
        public OutputWorshipCanEditAuthorization(UserManager<User> userManager, TypiconDBContext dbContext) : base(userManager, dbContext)
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                   IAuthorizationRequirement requirement,
                                   OutputWorshipCanEditKey resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.FromResult(0);
            }
            
            if (context.User.IsInRole(RoleConstants.AdministratorsRole))
            {
                // Administrators can do anything.
                context.Succeed(requirement);
            }
            else
            {
                //находим OutputDay
                var entity = DbContext.Set<OutputWorship>().FirstOrDefault(c => c.Id == resource.Id);

                if (entity == null)
                {
                    //Не нашли объект
                    context.Fail();
                }
                else
                {
                    var id = UserManager.GetUserId(context.User);

                    //Только Редакторы могут редактировать выходные формы
                    if (entity.OutputDay.Typicon.OwnerId.ToString() == id
                        || entity.OutputDay.Typicon.EditableUserTypicons.Any(c => c.UserId.ToString() == id))
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.FromResult(0);
        }
    }
}
