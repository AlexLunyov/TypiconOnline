using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Output;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Extensions
{
    public static class TypiconDBContextCommandExtensions
    {
        public static async Task ClearModifiedYearsAsync(this TypiconDBContext dbContext, int versionId)
        {
            int numberOfRowDeleted = await dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM ModifiedYear WHERE TypiconVersionId={versionId}");
        }

        public static async Task ClearOutputFormsAsync(this TypiconDBContext dbContext, int typiconId, bool deleteModifiedOutputDays)
        {
            //var query = $"DELETE FROM OUTPUTDAY WHERE TypiconId={typiconId} ";
            //int numberOfRowDeleted = await dbContext.Database.ExecuteSqlRawAsync(query);

            var days = dbContext.Set<OutputDay>().Where(c => c.TypiconId == typiconId);

            //если оставляем измененные вручную выходные фомры
            if (!deleteModifiedOutputDays)
            {
                //выбираем только те, у которых ModifiedDate == null
                //и не существует изменных вручную дочерних служб
                days = days.Where(c => c.ModifiedDate == null)
                    .Where(c => !c.Worships.Any(e => e.ModifiedDate != null));
            }

            //var list = days.ToList();

            dbContext.Set<OutputDay>().RemoveRange(days.ToList());

            await dbContext.SaveChangesAsync();
        }

        public static async Task ClearRuleErrorsAsync(this TypiconDBContext dbContext, int entityId)
        {
            int numberOfRowDeleted = await dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM TypiconVersionError WHERE EntityId={entityId}");
        }

        public static async Task ClearTypiconVersionErrorsAsync(this TypiconDBContext dbContext, int typiconVersionId)
        {
            int numberOfRowDeleted = await dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM TypiconVersionError WHERE TypiconVersionId={typiconVersionId}");
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
