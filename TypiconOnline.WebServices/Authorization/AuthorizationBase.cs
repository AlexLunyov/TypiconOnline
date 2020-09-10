using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.WebServices.Authorization
{
    public abstract class AuthorizationBase<T>: AuthorizationHandler<IAuthorizationRequirement, T>
        where T: class//, new()
    {
        public AuthorizationBase(UserManager<User> userManager
            , TypiconDBContext dbContext)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected UserManager<User> UserManager { get; }

        protected TypiconDBContext DbContext { get; }
    }
}
