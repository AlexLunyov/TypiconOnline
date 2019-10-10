using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
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
            
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            //SQLite connection
            var connectionString = $"FileName={TestContext.CurrentContext.TestDirectory}\\Data\\SQLiteDB.db";
            optionsBuilder.UseSqlite(connectionString);

            //MySQL connection
            //optionsBuilder.UseMySql("server=localhost;UserId=root;Password=admin;database=typicondb_test;charset=utf8mb4",
            //    mySqlOptions =>
            //    {
            //        mySqlOptions.ServerVersion(new Version(5, 6, 43), ServerType.MySql);
            //        mySqlOptions.AnsiCharSet(CharSet.Utf8mb4);
            //        mySqlOptions.UnicodeCharSet(CharSet.Utf8mb4);
            //    });

            optionsBuilder.EnableSensitiveDataLogging();

            return new TypiconDBContext(optionsBuilder.Options);
        }
    }
}
