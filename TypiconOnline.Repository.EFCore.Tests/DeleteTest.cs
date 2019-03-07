using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Repository.EFCore.Tests
{
    [TestClass]
    public class DeleteTest
    {
        [TestMethod]
        public void Delete_Test()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            //SQLite connection
            var connectionString = @"FileName=data\SQLiteDB.db";
            optionsBuilder.UseSqlite(connectionString);

            var context = new TypiconDBContext(optionsBuilder.Options);

            var years = context.Set<ModifiedYear>()
                .Where(c => c.TypiconVersionId == 1)
                .Include(c => c.ModifiedRules)
                .AsNoTracking();

            if (years != null)
            {
                context.Set<ModifiedYear>().RemoveRange(years);
            }
        }
    }
}
