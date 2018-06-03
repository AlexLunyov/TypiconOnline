using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Standard.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Typicon
{
    [TestFixture]
    public class MenologyRuleRepositoryTest
    {
        [Test]
        public void MenologyRuleRepository_Get()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();
            var connectionString = $"FileName={TestContext.CurrentContext.TestDirectory}\\Data\\SQLiteDB.db";
            optionsBuilder.UseSqlite(connectionString);

            optionsBuilder.EnableSensitiveDataLogging();

            var db = new TypiconDBContext(optionsBuilder.Options);

            var repo = new MenologyRuleRepository(db);

            var menologyRule = repo.Get(1, DateTime.Today);

            Assert.IsNotNull(menologyRule);
        }

        [Test]
        public void MenologyRuleRepository_GetFromServer()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();
            var connectionString = $"Data Source=31.31.196.160;Initial Catalog=u0351320_TypiconDB;Integrated Security=False;User Id=u0351320_defaultuser;Password=DDOR0YUMg519DbT2ebzN;MultipleActiveResultSets=True";
            optionsBuilder.UseSqlServer(connectionString);

            optionsBuilder.EnableSensitiveDataLogging();

            var db = new TypiconDBContext(optionsBuilder.Options);

            var repo = new MenologyRuleRepository(db);

            var menologyRule = repo.Get(1, DateTime.Today);

            Assert.IsNotNull(menologyRule);
        }

        [Test]
        public void MenologyRuleRepository_GetFromUOW()
        {
            var repo = new MenologyRuleRepository(UnitOfWorkFactory.Create());

            var menologyRule = repo.GetFromUOW(1, DateTime.Today);

            Assert.IsNotNull(menologyRule);
        }
    }
}
