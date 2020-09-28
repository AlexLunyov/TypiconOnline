using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Output;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Extensions
{
    public static class UpdateExtensions
    {
        public static async Task UpdateOutputFormAsync(this TypiconDBContext dbContext, OutputDay outputDay)
        {
            var outputDays = dbContext.Set<OutputDay>()
                .Where(c => c.TypiconId == outputDay.TypiconId && c.Date.Date == outputDay.Date.Date);

            if (outputDays.Any())
            {
                dbContext.Set<OutputDay>().RemoveRange(outputDays);
            }

            dbContext.Set<OutputDay>().Add(outputDay);

            await dbContext.SaveChangesAsync();
        }

        public static void UpdateOutputDay(this TypiconDBContext dbContext, OutputDay outputDay)
        {
            var outputDays = dbContext.Set<OutputDay>()
                .Where(c => c.TypiconId == outputDay.TypiconId && c.Date.Date == outputDay.Date.Date);

            if (outputDays.Any())
            {
                dbContext.Set<OutputDay>().RemoveRange(outputDays);
            }

            dbContext.Set<OutputDay>().Add(outputDay);

            dbContext.SaveChanges();
        }


        public static void UpdateModifiedYear(this TypiconDBContext dbContext, ModifiedYear modifiedYear)
        {
            dbContext.Set<ModifiedYear>().Update(modifiedYear);

            dbContext.SaveChanges();
        }

        public static async Task UpdateModifiedYearAsync(this TypiconDBContext dbContext, ModifiedYear modifiedYear)
        {
            dbContext.Set<ModifiedYear>().Update(modifiedYear);

            await dbContext.SaveChangesAsync();
        }

        public static async Task UpdateTypiconVersionAsync(this TypiconDBContext dbContext, TypiconVersion version)
        {
            dbContext.Set<TypiconVersion>().Update(version);

            await dbContext.SaveChangesAsync();
        }

        public static async Task UpdateTypiconEntityAsync(this TypiconDBContext dbContext, TypiconEntity typiconEntity)
        {
            dbContext.Set<TypiconEntity>().Update(typiconEntity);

            await dbContext.SaveChangesAsync();
        }

        

        public static async Task UpdateTypiconClaimAsync(this TypiconDBContext dbContext, TypiconClaim claim)
        {
            dbContext.Set<TypiconClaim>().Update(claim);

            await dbContext.SaveChangesAsync();
        }
    }
}
