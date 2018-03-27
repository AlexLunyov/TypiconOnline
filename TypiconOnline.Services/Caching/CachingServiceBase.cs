using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.AppServices.Caching
{
    public abstract class CachingServiceBase
    {
        protected readonly ICacheStorage cacheStorage;
        protected readonly IConfigurationRepository configurationRepository;

        public CachingServiceBase(ICacheStorage cacheStorage, IConfigurationRepository configurationRepository)
        {
            this.cacheStorage = cacheStorage ?? throw new ArgumentNullException("ICacheStorage");
            this.configurationRepository = configurationRepository ?? throw new ArgumentNullException("IConfigurationRepository");
        }

        protected void Store(string key, object typicon)
        {
            int time = configurationRepository.GetConfigurationValue<int>("ShortCacheDuration");
            cacheStorage.Store(key, typicon, TimeSpan.FromMinutes(time));
        }
    }
}
