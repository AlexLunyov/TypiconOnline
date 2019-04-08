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

namespace TypiconOnline.AppServices.Implementations.Extensions
{
    public static class TypiconDBContextCommandExtensions
    {
        public static async Task UpdateOutputFormAsync(this TypiconDBContext dbContext, OutputForm outputForm)
        {
            var outputForms = dbContext.Set<OutputForm>()
                .Where(c => c.TypiconId == outputForm.TypiconId && c.Date.Date == outputForm.Date.Date);

            if (outputForms.Any())
            {
                dbContext.Set<OutputForm>().RemoveRange(outputForms);
            }

            dbContext.Set<OutputForm>().Add(outputForm);

            //dbContext.Set<OutputForm>().Update(outputForm);

            await dbContext.SaveChangesAsync();
        }

        public static void UpdateOutputForm(this TypiconDBContext dbContext, OutputForm outputForm)
        {
            var outputForms = dbContext.Set<OutputForm>()
                .Where(c => c.TypiconId == outputForm.TypiconId && c.Date.Date == outputForm.Date.Date);

            if (outputForms.Any())
            {
                dbContext.Set<OutputForm>().RemoveRange(outputForms);
            }

            dbContext.Set<OutputForm>().Add(outputForm);

            //dbContext.Set<OutputForm>().Update(outputForm);

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

        public static async Task UpdateTypiconVersionAsync(this TypiconDBContext dbContext, TypiconVersion typiconVersion)
        {
            dbContext.Set<TypiconVersion>().FirstOrDefault(c => c.Id == typiconVersion.Id);

            dbContext.Set<TypiconVersion>().Update(typiconVersion);

            await dbContext.SaveChangesAsync();
        }

        public static async Task ClearOutputFormsAsync(this TypiconDBContext dbContext, int typiconId)
        {
            var outputForms = dbContext.Set<OutputForm>().Where(c => c.TypiconId == typiconId);

            dbContext.Set<OutputForm>().RemoveRange(outputForms);

            await dbContext.SaveChangesAsync();
        }
    }
}
