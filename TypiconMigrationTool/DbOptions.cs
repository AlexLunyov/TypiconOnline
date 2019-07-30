using Microsoft.EntityFrameworkCore;
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
        public static void CofigureMsSql(DbContextOptionsBuilder c)
        {
            c.UseSqlServer(ConfigurationManager.ConnectionStrings["MSSql"].ConnectionString);
            c.EnableSensitiveDataLogging();
        }

        public static void CofigureSqlite(DbContextOptionsBuilder c)
        {
            c.UseSqlite(ConfigurationManager.ConnectionStrings["Sqlite"].ConnectionString);
            c.EnableSensitiveDataLogging();
        }

        public static void CofigurePostgre(DbContextOptionsBuilder c)
        {
            c.UseNpgsql(ConfigurationManager.ConnectionStrings["Postgre"].ConnectionString);
            c.EnableSensitiveDataLogging();
        }

        public static void CofigureMySql(DbContextOptionsBuilder c)
        {
            c.UseMySql(ConfigurationManager.ConnectionStrings["MySql"].ConnectionString,
                mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(5, 6, 43), ServerType.MySql);
                    mySqlOptions.AnsiCharSet(CharSet.Utf8mb4);
                    mySqlOptions.UnicodeCharSet(CharSet.Utf8mb4);
                });
            c.EnableSensitiveDataLogging();
        }
    }
}
