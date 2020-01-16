using JetBrains.Annotations;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает Id и Name Устава
    /// </summary>
    public class SearchUserQueryHandler : DbContextQueryBase, IQueryHandler<SearchUserQuery, IEnumerable<SearchUserModel>>
    {
        public SearchUserQueryHandler(TypiconDBContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<SearchUserModel> Handle([NotNull] SearchUserQuery query)
        {
            var found = from s in DbContext.Set<User>()
                        where ((EF.Functions.Like(s.FullName, $"%{query.Search}%")
                            || EF.Functions.Like(s.Email, $"%{query.Search}%")
                            || EF.Functions.Like(s.UserName, $"%{query.Search}%"))
                            && s.UserRoles.Any(c => c.Role.Name == RoleConstants.EditorsRole))
                        select new SearchUserModel() { Id = s.Id, Name = $"{s.FullName} ({s.Email})" };

            return found.ToList();
        }
    }
}
