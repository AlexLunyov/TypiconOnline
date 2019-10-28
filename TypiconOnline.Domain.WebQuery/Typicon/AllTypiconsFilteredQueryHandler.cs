using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllTypiconsFilteredQueryHandler : DbContextQueryBase, IQueryHandler<AllTypiconsFilteredQuery, Result<IQueryable<TypiconEntityFilteredModel>>>
    {
        UserManager<User> _userManager;

        public AllTypiconsFilteredQueryHandler(TypiconDBContext dbContext, UserManager<User> userManager) : base(dbContext)
        {
            _userManager = userManager ?? throw new System.ArgumentNullException(nameof(userManager));
        }

        public Result<IQueryable<TypiconEntityFilteredModel>> Handle([NotNull] AllTypiconsFilteredQuery query)
        {
            IQueryable<TypiconEntity> queryResult = DbContext.Set<TypiconEntity>();

            var user = _userManager.FindByIdAsync(query.UserId.ToString()).Result;

            if (user != null)
            {
                var isAdmin = _userManager.IsInRoleAsync(user, RoleConstants.AdministratorsRole).Result;

                //if not admin
                if (!isAdmin)
                {
                    queryResult = queryResult
                        .Where(c => c.OwnerId == query.UserId
                                   || c.EditableUserTypicons.Any(d => d.UserId == query.UserId));
                }

                var result = new List<TypiconEntityFilteredModel>();

                foreach (var typ in queryResult)
                {
                    var inProcess = typ.Status == TypiconStatus.Approving
                                    || typ.Status == TypiconStatus.Validating
                                    || typ.Status == TypiconStatus.Publishing;

                    var dto = new TypiconEntityFilteredModel()
                    {
                        Id = typ.Id,
                        //PublishedVersionId = vrs.Id,
                        Name = typ.Name.FirstOrDefault(query.Language).Text,
                        Status = typ.Status.ToString(),
                        Editable = typ.Status != TypiconStatus.WaitingApprovement && !inProcess,
                        Deletable = (isAdmin || typ.OwnerId == query.UserId) && !inProcess,
                        Approvable = isAdmin && typ.Status == TypiconStatus.WaitingApprovement
                    };

                    result.Add(dto);
                }

                return Result.Ok(result.AsQueryable());
            }
            else
            {
                return Result.Fail<IQueryable<TypiconEntityFilteredModel>>($"Пользователь с Id= {query.UserId} не был найден");
            }
            
        }

        private TypiconStatus GetStatus(TypiconEntity typicon)
        {
            TypiconStatus status = TypiconStatus.WaitingApprovement;

            if (typicon.Versions.Any(c => c.BDate != null && c.EDate == null))
            {
                status = TypiconStatus.Published;
            }
            else if (typicon.Versions.Any(c => c.BDate == null && c.EDate == null))
            {
                status = TypiconStatus.Draft;
            }

            return status;
        }
    }
}
