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
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Output;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.WebServices.Authorization
{

    public abstract class TypiconByRuleCanEditAuthorization<T> : AuthorizationBase<TypiconByRuleCanEditKey<T>> where T : class, ITypiconVersionChild, new()
    {
        public TypiconByRuleCanEditAuthorization(UserManager<User> userManager, TypiconDBContext dbContext) : base(userManager, dbContext)
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement, TypiconByRuleCanEditKey<T> resource)
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
                var entity = DbContext.Set<T>().FirstOrDefault(c => c.Id == resource.Id);

                if (entity == null
                    || entity.TypiconVersion == null
                    || entity.TypiconVersion.Typicon == null)
                {
                    //Не нашли объект
                    context.Fail();
                }
                else
                {
                    var id = UserManager.GetUserId(context.User);

                    //Только Редакторы могут редактировать 
                    if (entity.TypiconVersion.Typicon.OwnerId.ToString() == id
                        || entity.TypiconVersion.Typicon.EditableUserTypicons.Any(c => c.UserId.ToString() == id))
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.FromResult(0);
        }
    }

    public class MenologyTypAuth : TypiconByRuleCanEditAuthorization<MenologyRule>
    {
        public MenologyTypAuth(UserManager<User> userManager, TypiconDBContext dbContext) : base(userManager, dbContext)
        {
        }
    }

    public class TriodionTypAuth : TypiconByRuleCanEditAuthorization<TriodionRule>
    {
        public TriodionTypAuth(UserManager<User> userManager, TypiconDBContext dbContext) : base(userManager, dbContext)
        {
        }
    }

    public class SignTypAuth : TypiconByRuleCanEditAuthorization<Sign>
    {
        public SignTypAuth(UserManager<User> userManager, TypiconDBContext dbContext) : base(userManager, dbContext)
        {
        }
    }

    public class VariableTypAuth : TypiconByRuleCanEditAuthorization<TypiconVariable>
    {
        public VariableTypAuth(UserManager<User> userManager, TypiconDBContext dbContext) : base(userManager, dbContext)
        {
        }
    }
}
