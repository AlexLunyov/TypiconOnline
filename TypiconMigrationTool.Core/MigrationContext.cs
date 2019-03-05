using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconMigrationTool.Core
{
    public class Db : TypiconDBContext
    {
        public Db(DbContextOptions<TypiconDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Sqlite
            //optionsBuilder.UseSqlite(@"FileName=Data\SQLiteDB.db");

            //MSSql
            //optionsBuilder.UseSqlServer($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Directory.GetCurrentDirectory()}\Data\TypiconDB.mdf;Database=TypiconDB;Integrated Security=True;Trusted_Connection=True");

            //PostgreSQL
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=typicondb;Username=postgres;Password=z2LDCiiEQFDBlkl3eZyb");

            //для теста
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
