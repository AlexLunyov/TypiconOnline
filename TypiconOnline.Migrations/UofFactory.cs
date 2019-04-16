using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconMigrationTool.Typicon
{
    class UofFactory
    {
        private readonly IConfigurationRoot _configuration;
        public UofFactory(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public (IUnitOfWork, DbContextOptionsBuilder<TypiconDBContext>) GetMSSqlUnitOfWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MSSql"));
            optionsBuilder.EnableSensitiveDataLogging();

            var dbContext = new TypiconDBContext(optionsBuilder.Options);

            return (new UnitOfWork(dbContext, new RepositoryFactory()), optionsBuilder);
        }

        public (IUnitOfWork, DbContextOptionsBuilder<TypiconDBContext>) GetSQLiteUnitOfWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("Sqlite"));

            optionsBuilder.EnableSensitiveDataLogging();

            var dbContext = new TypiconDBContext(optionsBuilder.Options);

            return (new UnitOfWork(dbContext, new RepositoryFactory()), optionsBuilder);
        }

        public (IUnitOfWork, DbContextOptionsBuilder<TypiconDBContext>) GetPostgreSQLUnitOfWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Postgre"));
            optionsBuilder.EnableSensitiveDataLogging();

            var dbContext = new TypiconDBContext(optionsBuilder.Options);

            return (new UnitOfWork(dbContext, new RepositoryFactory()), optionsBuilder);
        }

        public (IUnitOfWork, DbContextOptionsBuilder<TypiconDBContext>) GetMySQLUnitOfWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            //optionsBuilder.UseMySql("server=localhost;UserId=root;Password=z2LDCiiEQFDBlkl3eZyb;database=typicondb;",
            optionsBuilder.UseMySql("server=31.31.196.160;UserId=u0351_mysqluser;Password=gl7fdQ45GZyqydXrr2BZ;database=u0351320_typicondb;",
            //optionsBuilder.UseMySql("server=31.31.196.160;UserId=u0351_buser;Password=gl7fdQ45GZyqydXrr2BZ;database=u0351320_berlukirudb;",
            mySqlOptions =>
            {
                mySqlOptions.ServerVersion(new Version(8, 0, 15), ServerType.MySql);
            });
            optionsBuilder.EnableSensitiveDataLogging();

            var dbContext = new TypiconDBContext(optionsBuilder.Options);

            return (new UnitOfWork(dbContext, new RepositoryFactory()), optionsBuilder);
        }
    }
}
