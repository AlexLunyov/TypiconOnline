using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconMigrationTool.Core
{
    public class Db : TypiconDBContext
    {
        public Db() : base(CreateOptions()) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //Sqlite
        //    optionsBuilder.UseSqlite(@"FileName=Data\SQLiteDB.db");

        //    //MSSql
        //    //optionsBuilder.UseSqlServer($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Directory.GetCurrentDirectory()}\Data\TypiconDB.mdf;Database=TypiconDB;Integrated Security=True;Trusted_Connection=True");

        //    //PostgreSQL
        //    //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=typicondb;Username=postgres;Password=z2LDCiiEQFDBlkl3eZyb");

        //    //для теста
        //    optionsBuilder.EnableSensitiveDataLogging();
        //}

        private static DbContextOptions<TypiconDBContext> CreateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();
            //Sqlite
            //optionsBuilder.UseSqlite(@"FileName=data\SQLiteDB.db");

            //MSSql
            //var connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Directory.GetCurrentDirectory()}\Data\TypiconDB.mdf;Database=TypiconDB;Integrated Security=True;Trusted_Connection=True";
            //optionsBuilder.UseSqlServer(connectionString);

            //PostgreSQL
            //optionsBuilder.UseNpgsql($@"Host=localhost;Port=5432;Database=typicondb;Username=postgres;Password=z2LDCiiEQFDBlkl3eZyb");

            //MySQL
            optionsBuilder.UseMySql("server=localhost;UserId=root;Password=z2LDCiiEQFDBlkl3eZyb;database=typicondb;",
                    mySqlOptions =>
                    {
                        mySqlOptions.ServerVersion(new Version(8, 0, 15), ServerType.MySql);
                    });

            return optionsBuilder.Options;
        }
    }
}
