using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Extensions
{
    public static class AddExtensions
    {
        public static async Task AddTypiconVersionAsync(this TypiconDBContext dbContext, TypiconVersion version)
        {
            dbContext.Set<TypiconVersion>().Add(version);

            var iHasIds = dbContext.ChangeTracker.Entries()
                .Where(c => c.State == EntityState.Added
                            && c.IsKeySet
                            && c.Entity is ITypiconVersionChild child
                            && child.TypiconVersionId == version.Id);
                //.Select(c => c.Entity as ITypiconVersionChild);

            foreach (var entry in iHasIds)
            {
                //entry.
                (entry.Entity as ITypiconVersionChild).Id = default(int);
                //entry.Id = default(int);
            }

            dbContext.Set<TypiconVersion>().Update(version);

            await dbContext.SaveChangesAsync();
        }

        public static async Task AddTypiconEntityAsync(this TypiconDBContext dbContext, TypiconEntity entity)
        {
            dbContext.Set<TypiconEntity>().Add(entity);

            await dbContext.SaveChangesAsync();
        }

        public static async Task RemoveTypiconClaimAsync(this TypiconDBContext dbContext, TypiconClaim claim)
        {
            dbContext.Set<TypiconClaim>().Remove(claim);

            await dbContext.SaveChangesAsync();
        }
    }
}
