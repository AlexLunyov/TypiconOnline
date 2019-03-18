using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Implementations.Extensions
{
    public static class TypiconDBContextCommandExtensions
    {
        public static void UpdateOutputForm(this TypiconDBContext dbContext, OutputForm outputForm)
        {
            dbContext.Set<OutputForm>().Update(outputForm);

            dbContext.SaveChanges();
        }
        

        public static void UpdateModifiedYear(this TypiconDBContext dbContext, ModifiedYear modifiedYear)
        {
            dbContext.Set<ModifiedYear>().Update(modifiedYear);

            dbContext.SaveChanges();
        }

        public static Task UpdateModifiedYearAsync(this TypiconDBContext dbContext, ModifiedYear modifiedYear)
        {
            dbContext.Set<ModifiedYear>().Update(modifiedYear);

            return dbContext.SaveChangesAsync();
        }
    }
}
