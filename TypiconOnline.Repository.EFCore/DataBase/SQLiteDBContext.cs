using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Repository.EFCore.DataBase
{
    public class SQLiteDBContext : DBContextBase
    {
        string connectionString;

        public SQLiteDBContext() : this(@"Filename=Data\SQLiteDB.db") { }

        public SQLiteDBContext(string connection)
        {
            connectionString = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
            //для теста
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
