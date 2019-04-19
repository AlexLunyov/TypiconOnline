using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Extensions
{
    public static class TypiconDBContextCommandExtensions
    {
        public static async Task ClearModifiedYearsAsync(this TypiconDBContext dbContext, int versionId)
        {
            int numberOfRowDeleted = await dbContext.Database.ExecuteSqlCommandAsync("DELETE FROM ModifiedYear WHERE TypiconVersionId={0}", versionId);
        }

        public static async Task ClearOutputFormsAsync(this TypiconDBContext dbContext, int typiconId)
        {
            int numberOfRowDeleted = await dbContext.Database.ExecuteSqlCommandAsync("DELETE FROM OutputForm WHERE TypiconId={0}", typiconId);
        }

        public static async Task ClearRuleErrorsAsync(this TypiconDBContext dbContext, int entityId)
        {
            int numberOfRowDeleted = await dbContext.Database.ExecuteSqlCommandAsync("DELETE FROM TypiconVersionError WHERE EntityId={0}", entityId);
        }

        public static async Task ClearTypiconVersionErrorsAsync(this TypiconDBContext dbContext, int typiconVersionId)
        {
            int numberOfRowDeleted = await dbContext.Database.ExecuteSqlCommandAsync("DELETE FROM TypiconVersionError WHERE TypiconVersionId={0}", typiconVersionId);
        }

        public static async Task AddTypiconVersionErrorAsync(this TypiconDBContext dbContext, TypiconVersionError error)
        {
            await dbContext.Set<TypiconVersionError>().AddAsync(error);
            await dbContext.SaveChangesAsync();
        }

        public static async Task AddTypiconVersionErrorAsync(this TypiconDBContext dbContext, IEnumerable<TypiconVersionError> errors)
        {
            await dbContext.Set<TypiconVersionError>().AddRangeAsync(errors);
            await dbContext.SaveChangesAsync();
        }
    }
}
