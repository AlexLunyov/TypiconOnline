﻿using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Repository.EFCore;

namespace TypiconOnline.AppServices.Tests
{
    public class UnitOfWorkFactory
    {
        public static IUnitOfWork Create()
        {
            //SQLite connection
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();
            var connectionString = $"FileName={TestContext.CurrentContext.TestDirectory}\\Data\\SQLiteDB.db";
            optionsBuilder.UseSqlite(connectionString);

            var dbContext = new CachedDbContext(optionsBuilder.Options);

            return new UnitOfWork(dbContext);
        }
    }
}