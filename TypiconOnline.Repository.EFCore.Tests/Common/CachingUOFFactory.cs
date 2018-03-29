using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Standard.Caching;
using TypiconOnline.AppServices.Standard.Configuration;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Repository.EFCore.Caching;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Repository.EFCore.Tests.Common
{
    public class CachingUOFFactory
    {
        public static UnitOfWork Create()
        {
            string path = $"FileName={Path.Combine(TestContext.CurrentContext.TestDirectory, @"Data\SQLiteDB.db")}";
            var context = new SQLiteDBContext(path);
            var cacheStorage = new MemoryCacheStorage(new MemoryCache(new MemoryCacheOptions()));
            var configurationRepository = new TestConfRepository();
            var cachingRepository = new CachingRepositoryFactory(new RepositoryFactory(context), cacheStorage, configurationRepository);

            return new UnitOfWork(context, cachingRepository);
        }
    }

    public class TestConfRepository : IConfigurationRepository
    {
        public T GetConfigurationValue<T>(string key)
        {
            return (T)(object)60;
        }

        public string GetConfigurationValue(string key)
        {
            throw new NotImplementedException();
        }

        public T GetConfigurationValue<T>(string key, T defaultValue)
        {
            throw new NotImplementedException();
        }

        public string GetConfigurationValue(string key, string defaultValue)
        {
            throw new NotImplementedException();
        }
    }
}
