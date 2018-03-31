using EFSecondLevelCache.Core;
using EFSecondLevelCache.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Repository.EFCore.DataBase
{
    public class MSSqlDBContext : DBContextBase
    {
        private string _databasePath { get; set; }

        //public MSSqlDBContext() : this ("DBTypicon") { }

        public MSSqlDBContext(string connection)
        {
            _databasePath = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databasePath);

            //для теста
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
