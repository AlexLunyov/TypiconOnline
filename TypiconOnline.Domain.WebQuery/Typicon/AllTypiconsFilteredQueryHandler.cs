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
            var user = _userManager.FindByIdAsync(query.UserId.ToString()).Result;

            if (user != null)
            {
                var isAdmin = _userManager.IsInRoleAsync(user, RoleConstants.AdministratorsRole).Result;

                var result = new List<TypiconEntityFilteredModel>();

                #region TypiconVersions

                //черновики
                IQueryable<TypiconVersion> versResult = DbContext
                    .Set<TypiconVersion>()
                    .Where(c => c.BDate == null && c.EDate == null);

                //if not admin
                if (!isAdmin)
                {
                    versResult = versResult
                        .Where(c => c.Typicon.OwnerId == query.UserId
                                   || c.Typicon.EditableUserTypicons.Any(d => d.UserId == query.UserId));
                }

                foreach (var typ in versResult.ToList())
                {
                    var inProcess = typ.Typicon.Status == TypiconStatus.Approving
                                    || typ.Typicon.Status == TypiconStatus.Validating
                                    || typ.Typicon.Status == TypiconStatus.Publishing;

                    var editable = typ.Typicon.Status != TypiconStatus.WaitingApprovement && !inProcess;

                    var dto = new TypiconEntityFilteredModel()
                    {
                        Id = typ.TypiconId,
                        //PublishedVersionId = vrs.Id,
                        Name = typ.Name.FirstOrDefault(query.Language).Text,
                        SystemName = typ.Typicon.SystemName,
                        Status = typ.Typicon.Status.ToString(),
                        IsTemplate = typ.IsTemplate,
                        Editable = editable,
                        DeleteLink = ((isAdmin || typ.Typicon.OwnerId == query.UserId) && !inProcess)
                            ? "Delete"
                            : default,
                        Reviewable = false, //уже все рассмотрены и одобрены
                        Exportable = isAdmin && editable
                    };

                    result.Add(dto);
                }

                #endregion

                #region TypiconClaims

                IQueryable<TypiconClaim> claimsResult = DbContext.Set<TypiconClaim>()
                    .Where(c => c.Status != TypiconClaimStatus.InProcess);

                //if not admin
                if (!isAdmin)
                {
                    claimsResult = claimsResult.Where(c => c.OwnerId == query.UserId);
                }

                foreach (var claim in claimsResult)
                {
                    var dto = new TypiconEntityFilteredModel()
                    {
                        Id = claim.Id,
                        Name = claim.Name.FirstOrDefault(query.Language).Text,
                        SystemName = claim.SystemName,
                        Status = claim.Status.ToString(),
                        Editable = false,
                        DeleteLink = (isAdmin || claim.OwnerId == query.UserId)
                            ? "DeleteClaim"
                            : default,
                        Reviewable = isAdmin && claim.Status == TypiconClaimStatus.WatingForReview
                    };

                    result.Add(dto);
                }

                #endregion

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
