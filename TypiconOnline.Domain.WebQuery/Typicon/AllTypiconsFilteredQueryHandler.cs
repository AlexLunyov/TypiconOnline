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
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllTypiconsFilteredQueryHandler : DbContextQueryBase, IDataQueryHandler<AllTypiconsFilteredQuery, IEnumerable<TypiconEntityFilteredModel>>
    {
        UserManager<User> _userManager;

        public AllTypiconsFilteredQueryHandler(TypiconDBContext dbContext, UserManager<User> userManager) : base(dbContext)
        {
            _userManager = userManager ?? throw new System.ArgumentNullException(nameof(userManager));
        }

        public IEnumerable<TypiconEntityFilteredModel> Handle([NotNull] AllTypiconsFilteredQuery query)
        {
            IQueryable<TypiconEntity> queryResult = DbContext.Set<TypiconEntity>();

            var user = _userManager.FindByIdAsync(query.UserId.ToString()).Result;

            var isAdmin = _userManager.IsInRoleAsync(user, RoleConstants.AdministratorsRole).Result;

            //if not admin
            if (!isAdmin)
            {
                queryResult = queryResult.Where(c => c.OwnerId == query.UserId)
                    .Where(c => c.EditableUserTypicons.Any(d => d.UserId == query.UserId));
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
                    Editable = !inProcess,
                    Deletable = (isAdmin || typ.OwnerId == query.UserId) && !inProcess,
                    Approvable = isAdmin && typ.Status == TypiconStatus.WaitingApprovement
                };

                result.Add(dto);
            }

            return result.AsEnumerable();
        }

        private TypiconStatus GetStatus(TypiconEntity typicon)
        {
            TypiconStatus status = TypiconStatus.WaitingApprovement;

            if (typicon.Versions.Any(c => c.IsPublished))
            {
                status = TypiconStatus.Published;
            }
            else if (typicon.Versions.Any(c => c.IsDraft))
            {
                status = TypiconStatus.Draft;
            }

            return status;
        }
    }
}
