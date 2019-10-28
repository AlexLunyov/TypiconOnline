using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconMigrationTool
{
    public class DbOptions
    {
        public static void ConfigureMsSql(DbContextOptionsBuilder c, IConfiguration config)
        {
            c.UseSqlServer(config.GetConnectionString("MsSql"));
            c.EnableSensitiveDataLogging();
        }

        public static void ConfigureSqlite(DbContextOptionsBuilder c, IConfiguration config)
        {
            c.UseSqlite(config.GetConnectionString("Sqlite"));
            c.EnableSensitiveDataLogging();
        }

        public static void ConfigurePostgre(DbContextOptionsBuilder c, IConfiguration config)
        {
            c.UseNpgsql(config.GetConnectionString("Postgre"));
            c.EnableSensitiveDataLogging();
        }

        public static void ConfigureMySql(DbContextOptionsBuilder c, IConfiguration config)
        {
            c.UseMySql(config.GetConnectionString("MySql"),
                mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(5, 6, 44), ServerType.MySql);
                    mySqlOptions.AnsiCharSet(CharSet.Utf8mb4);
                    mySqlOptions.UnicodeCharSet(CharSet.Utf8mb4);
                });
            c.EnableSensitiveDataLogging();
        }
    }
}
