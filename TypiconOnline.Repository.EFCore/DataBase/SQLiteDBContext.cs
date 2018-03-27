using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Repository.EFCore.DataBase
{
    public class SQLiteDBContext : DBContextBase
    {
        private string _databasePath { get; set; }

        //public SQLiteDBContext()
        //{
        //    _databasePath = @"Data\SQLiteDB.db";
        //}

        public SQLiteDBContext(string connection)
        {
            _databasePath = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
            //для теста
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
