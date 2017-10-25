using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Infrastructure.Win.Caching;
using TypiconOnline.Infrastructure.Win.Configuration;

namespace TypiconOnline.WinServices.Implementations
{
    public class EnrichedTypiconEntityService : ITypiconEntityService
    {
        private readonly ITypiconEntityService _innerTypiconEntityService;
        private readonly ICacheStorage _cacheStorage;
        private readonly IConfigurationRepository _configurationRepository;

        private const string getTypiconEntityKey = "GetTypiconEntity";

        public EnrichedTypiconEntityService(ITypiconEntityService innerTypiconEntityService, ICacheStorage cacheStorage
            , IConfigurationRepository configurationRepository)
        {
            if (innerTypiconEntityService == null) throw new ArgumentNullException("TypiconEntityService");
            if (cacheStorage == null) throw new ArgumentNullException("CacheStorage");
            if (configurationRepository == null) throw new ArgumentNullException("ConfigurationRepository");
            _innerTypiconEntityService = innerTypiconEntityService;
            _cacheStorage = cacheStorage;
            _configurationRepository = configurationRepository;
        }

        public void ClearModifiedYears(int id)
        {
            _innerTypiconEntityService.ClearModifiedYears(id);
        }

        public void ReloadRules(int id, string folderPath)
        {
            _innerTypiconEntityService.ReloadRules(id, folderPath);
        }

        public GetTypiconEntityResponse GetTypiconEntity(int id)
        {
            string key = getTypiconEntityKey + id;
            GetTypiconEntityResponse response = _cacheStorage.Retrieve<GetTypiconEntityResponse>(key); 
            if (response == null)
            {
                int cacheDurationSeconds = _configurationRepository.GetConfigurationValue<int>("ShortCacheDuration");
                response = _innerTypiconEntityService.GetTypiconEntity(id);
                _cacheStorage.Store(key, response, TimeSpan.FromSeconds(cacheDurationSeconds));
            }
            return response;
        }

        public GetTypiconEntitiesResponse GetAllTypiconEntities()
        {
            string key = "GetAllTypiconEntities";
            GetTypiconEntitiesResponse response = _cacheStorage.Retrieve<GetTypiconEntitiesResponse>(key);
            if (response == null)
            {
                int cacheDurationSeconds = _configurationRepository.GetConfigurationValue<int>("ShortCacheDuration");
                response = _innerTypiconEntityService.GetAllTypiconEntities();
                _cacheStorage.Store(key, response, TimeSpan.FromSeconds(cacheDurationSeconds));
            }
            return response;
        }

        public InsertTypiconEntityResponse InsertTypiconEntity(InsertTypiconEntityRequest insertTypiconEntityRequest)
        {
            throw new NotImplementedException();
            //return _innerTypiconEntityService.InsertTypiconEntity(insertTypiconEntityRequest);
        }

        public UpdateTypiconEntityResponse UpdateTypiconEntity(UpdateTypiconEntityRequest updateTypiconEntityRequest)
        {
            throw new NotImplementedException();
            //return _innerTypiconEntityService.UpdateTypiconEntity(updateTypiconEntityRequest);
        }

        public DeleteTypiconEntityResponse DeleteTypiconEntity(DeleteTypiconEntityRequest deleteTypiconEntityRequest)
        {
            throw new NotImplementedException();
            //return _innerTypiconEntityService.DeleteTypiconEntity(deleteTypiconEntityRequest);
        }
    }
}
