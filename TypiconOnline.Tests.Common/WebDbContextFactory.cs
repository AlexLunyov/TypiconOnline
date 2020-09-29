using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Context;

namespace TypiconOnline.Tests.Common
{
    public class WebDbContextFactory
    {
        public static WebDbContext Create()
        {
            return new WebDbContext(GetOptions());
        }

        //public static EventsTypiconDBContext CreateWithEvents()
        //{
        //    var container = new SIContainer(withEvents: true);

        //    var dispatcher = container.GetInstance<IEventDispatcher>();

        //    return new EventsTypiconDBContext(dispatcher, GetOptions());
        //}

        private static DbContextOptions<WebDbContext> GetOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebDbContext>();

            //SQLite connection
            //var connectionString = $"FileName={TestContext.CurrentContext.TestDirectory}\\Data\\SQLiteDB.db";
            //optionsBuilder.UseSqlite(connectionString);

            //MySQL connection
            optionsBuilder.UseMySql("server=localhost;UserId=root;Password=admin;database=typicondb;charset=utf8mb4",
                mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(5, 6, 44), ServerType.MySql);
                    //mySqlOptions.AnsiCharSet(CharSet.Utf8mb4);
                    //mySqlOptions.UnicodeCharSet(CharSet.Utf8mb4);
                });

            optionsBuilder.EnableSensitiveDataLogging();

            return optionsBuilder.Options;
        }
    }
}
