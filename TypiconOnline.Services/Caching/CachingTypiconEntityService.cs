using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.AppServices.Caching
{
    /// <summary>
    /// Служба для CRUD операций над сущностью TypiconEntity, реализующая кеширование
    /// </summary>
    public class CachingTypiconEntityService : CachingServiceBase, ITypiconEntityService
    {
        readonly ITypiconEntityService service;

        const string KEY_TYPICON = "GetTypiconEntity";
        const string KEY_ALL = "GetAllTypiconEntities";

        public CachingTypiconEntityService(ITypiconEntityService service, ICacheStorage cacheStorage
            , IConfigurationRepository configurationRepository) : base(cacheStorage, configurationRepository)
        {
            this.service = service ?? throw new ArgumentNullException("TypiconEntityService");
        }

        public void ClearModifiedYears(int id)
        {
            service.ClearModifiedYears(id);
        }

        public void ReloadRules(int id, string folderPath)
        {
            service.ReloadRules(id, folderPath);
        }

        public GetTypiconEntityResponse GetTypiconEntity(int id)
        {
            string key = KEY_TYPICON + id;
            var typicon = cacheStorage.Retrieve<TypiconEntity>(key); 
            if (typicon == null)
            {
                var response = service.GetTypiconEntity(id);
                typicon = response.TypiconEntity;
                Store(key, typicon);

                return response;
            }
            return new GetTypiconEntityResponse() { TypiconEntity = typicon };
        }

        public GetTypiconEntitiesResponse GetAllTypiconEntities()
        {
            var response = cacheStorage.Retrieve<GetTypiconEntitiesResponse>(KEY_ALL);
            if (response == null)
            {
                response = service.GetAllTypiconEntities();

                Store(KEY_ALL, response);
            }
            return response;
        }

        public InsertTypiconEntityResponse InsertTypiconEntity(InsertTypiconEntityRequest insertTypiconEntityRequest)
        {
            string key = KEY_TYPICON + insertTypiconEntityRequest.TypiconEntity.Id;

            Store(key, insertTypiconEntityRequest.TypiconEntity);

            return service.InsertTypiconEntity(insertTypiconEntityRequest);
        }

        public UpdateTypiconEntityResponse UpdateTypiconEntity(UpdateTypiconEntityRequest updateTypiconEntityRequest)
        {
            string key = KEY_TYPICON + updateTypiconEntityRequest.TypiconEntity.Id;

            Store(key, updateTypiconEntityRequest.TypiconEntity);

            return service.UpdateTypiconEntity(updateTypiconEntityRequest);
        }

        public DeleteTypiconEntityResponse DeleteTypiconEntity(DeleteTypiconEntityRequest deleteTypiconEntityRequest)
        {
            throw new NotImplementedException();
            //return _innerTypiconEntityService.DeleteTypiconEntity(deleteTypiconEntityRequest);
        }
    }
}
