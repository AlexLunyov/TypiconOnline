using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconMigrationTool.Typicon
{
    class UofFactory
    {
        public UofFactory()
        {
        }

        public (IUnitOfWork, TypiconDBContext, Action<DbContextOptionsBuilder>) GetMSSqlUnitOfWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            DbOptions.CofigureMsSql(optionsBuilder);

            var dbContext = new TypiconDBContext(optionsBuilder.Options);

            return (new UnitOfWork(dbContext, new RepositoryFactory()), dbContext, DbOptions.CofigureMsSql);
        }

        public (IUnitOfWork, TypiconDBContext, Action<DbContextOptionsBuilder>) GetSQLiteUnitOfWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            DbOptions.CofigureSqlite(optionsBuilder);

            var dbContext = new TypiconDBContext(optionsBuilder.Options);

            return (new UnitOfWork(dbContext, new RepositoryFactory()), dbContext, DbOptions.CofigureSqlite);
        }

        public (IUnitOfWork, TypiconDBContext, Action<DbContextOptionsBuilder>) GetPostgreSQLUnitOfWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            DbOptions.CofigurePostgre(optionsBuilder);

            var dbContext = new TypiconDBContext(optionsBuilder.Options);

            return (new UnitOfWork(dbContext, new RepositoryFactory()), dbContext, DbOptions.CofigurePostgre);
        }

        public (IUnitOfWork, TypiconDBContext, Action<DbContextOptionsBuilder>) GetMySQLUnitOfWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            DbOptions.CofigureMySql(optionsBuilder);

            var dbContext = new TypiconDBContext(optionsBuilder.Options);

            return (new UnitOfWork(dbContext, new RepositoryFactory()), dbContext, DbOptions.CofigureMySql);


        }
    }
}
