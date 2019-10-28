using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconMigrationTool
{
    class UofFactory
    {
        public UofFactory()
        {
        }

        public (IUnitOfWork, TypiconDBContext, Action<DbContextOptionsBuilder, IConfiguration>) GetMSSqlUnitOfWork(IConfiguration config)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            DbOptions.ConfigureMsSql(optionsBuilder, config);

            var dbContext = new TypiconDBContext(optionsBuilder.Options);

            return (new UnitOfWork(dbContext, new RepositoryFactory()), dbContext, DbOptions.ConfigureMsSql);
        }

        public (IUnitOfWork, TypiconDBContext, Action<DbContextOptionsBuilder, IConfiguration>) GetSQLiteUnitOfWork(IConfiguration config)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            DbOptions.ConfigureSqlite(optionsBuilder, config);

            var dbContext = new TypiconDBContext(optionsBuilder.Options);

            return (new UnitOfWork(dbContext, new RepositoryFactory()), dbContext, DbOptions.ConfigureSqlite);
        }

        public (IUnitOfWork, TypiconDBContext, Action<DbContextOptionsBuilder, IConfiguration>) GetPostgreSQLUnitOfWork(IConfiguration config)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            DbOptions.ConfigurePostgre(optionsBuilder, config);

            var dbContext = new TypiconDBContext(optionsBuilder.Options);

            return (new UnitOfWork(dbContext, new RepositoryFactory()), dbContext, DbOptions.ConfigurePostgre);
        }

        public (IUnitOfWork, TypiconDBContext, Action<DbContextOptionsBuilder, IConfiguration>) GetMySQLUnitOfWork(IConfiguration config)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            DbOptions.ConfigureMySql(optionsBuilder, config);

            var dbContext = new TypiconDBContext(optionsBuilder.Options);

            return (new UnitOfWork(dbContext, new RepositoryFactory()), dbContext, DbOptions.ConfigureMySql);


        }
    }
}
