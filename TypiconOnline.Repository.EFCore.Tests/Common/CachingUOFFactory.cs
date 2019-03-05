﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Caching;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.Caching;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Repository.EFCore.Tests.Common
{
    public class CachingUOFFactory
    {
        public static IUnitOfWork Create() => Create(new RepositoryFactory());

        public static IUnitOfWork Create(IRepositoryFactory repository)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            //MS SQL
            //string path = $"Data Source=31.31.196.160;Initial Catalog=u0351320_Typicon;Integrated Security=False;User Id=u0351320_defaultuser;Password=DDOR0YUMg519DbT2ebzN;MultipleActiveResultSets=True";
            //string path = $"Data Source=(LocalDB)\\MSSQLLocalDB;Database=TypiconDB;Integrated Security=True;Connect Timeout=30";
            //optionsBuilder.UseSqlServer(path);

            //PostgreSQL
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=typicondb;Username=postgres;Password=z2LDCiiEQFDBlkl3eZyb");

            var dbContext = new FakeDbContext(optionsBuilder.Options);

            var cacheStorage = new MemoryCacheStorage(new MemoryCache(new MemoryCacheOptions()));

            var confCacheDuration = Mock.Of<IConfigurationRepository>(c => c.GetConfigurationValue<int>("ShortCacheDuration") == 60);

            var repositoryFactory = new CachingRepositoryFactory(repository, cacheStorage, confCacheDuration);

            return new UnitOfWork(dbContext, repositoryFactory);
        }
    }
}
