using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Служба для CRUD операций над сущностью TypiconEntity, реализующая кеширование
    /// </summary>
    public class CachingTypiconEntityService : ITypiconEntityService
    {
        private readonly ITypiconEntityService _innerTypiconEntityService;
        private readonly ICacheStorage _cacheStorage;
        private readonly IConfigurationRepository _configurationRepository;

        private const string getTypiconEntityKey = "GetTypiconEntity";

        public CachingTypiconEntityService(ITypiconEntityService innerTypiconEntityService, ICacheStorage cacheStorage
            , IConfigurationRepository configurationRepository)
        {
            _innerTypiconEntityService = innerTypiconEntityService ?? throw new ArgumentNullException("TypiconEntityService");
            _cacheStorage = cacheStorage ?? throw new ArgumentNullException("CacheStorage");
            _configurationRepository = configurationRepository ?? throw new ArgumentNullException("ConfigurationRepository");
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
            var typicon = _cacheStorage.Retrieve<TypiconEntity>(key); 
            if (typicon == null)
            {
                var response = _innerTypiconEntityService.GetTypiconEntity(id);
                typicon = response.TypiconEntity;
                Store(key, typicon);

                return response;
            }
            return new GetTypiconEntityResponse() { TypiconEntity = typicon };
        }

        public GetTypiconEntitiesResponse GetAllTypiconEntities()
        {
            string key = "GetAllTypiconEntities";
            var response = _cacheStorage.Retrieve<GetTypiconEntitiesResponse>(key);
            if (response == null)
            {
                response = _innerTypiconEntityService.GetAllTypiconEntities();

                Store(key, response);
            }
            return response;
        }

        public InsertTypiconEntityResponse InsertTypiconEntity(InsertTypiconEntityRequest insertTypiconEntityRequest)
        {
            string key = getTypiconEntityKey + insertTypiconEntityRequest.TypiconEntity.Id;

            Store(key, insertTypiconEntityRequest.TypiconEntity);

            return _innerTypiconEntityService.InsertTypiconEntity(insertTypiconEntityRequest);
        }

        public UpdateTypiconEntityResponse UpdateTypiconEntity(UpdateTypiconEntityRequest updateTypiconEntityRequest)
        {
            string key = getTypiconEntityKey + updateTypiconEntityRequest.TypiconEntity.Id;

            Store(key, updateTypiconEntityRequest.TypiconEntity);

            return _innerTypiconEntityService.UpdateTypiconEntity(updateTypiconEntityRequest);
        }

        public DeleteTypiconEntityResponse DeleteTypiconEntity(DeleteTypiconEntityRequest deleteTypiconEntityRequest)
        {
            throw new NotImplementedException();
            //return _innerTypiconEntityService.DeleteTypiconEntity(deleteTypiconEntityRequest);
        }

        #region Private Methods

        /// <summary>
        /// Храним в кеше саму сущность TypiconEntity
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void Store(string key, object typicon)
        {
            int time = _configurationRepository.GetConfigurationValue<int>("ShortCacheDuration");
            _cacheStorage.Store(key, typicon, TimeSpan.FromMinutes(time));
        }

        #endregion
    }
}
