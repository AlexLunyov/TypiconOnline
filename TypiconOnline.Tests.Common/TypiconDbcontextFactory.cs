using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Tests.Common
{
    public class TypiconDbContextFactory
    {
        public static TypiconDBContext Create()
        {
            //SQLite connection
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();
            var connectionString = $"FileName={TestContext.CurrentContext.TestDirectory}\\Data\\SQLiteDB.db";
            optionsBuilder.UseSqlite(connectionString);

            optionsBuilder.EnableSensitiveDataLogging();

            return new TypiconDBContext(optionsBuilder.Options);
        }
    }
}
