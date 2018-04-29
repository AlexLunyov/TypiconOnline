using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconMigrationTool.Core
{
    //public class SQLite : TypiconDBContext
    //{
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.UseSqlite(@"FileName=Data\SQLiteDB.db");
    //        //для теста
    //        optionsBuilder.EnableSensitiveDataLogging();
    //    }
    //}
    public class MSSql : TypiconDBContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Directory.GetCurrentDirectory()}\Data\TypiconDB.mdf;Database=TypiconDB;Integrated Security=True;Trusted_Connection=True");

            //для теста
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
