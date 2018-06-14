using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Typicon;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface ITypiconEntityService
    {
        void ClearModifiedYears(int id);
        void ReloadRules(int id, string folderPath);
        GetTypiconEntityResponse GetTypiconEntity(int id);
        GetTypiconEntitiesResponse GetAllTypiconEntities();
        InsertTypiconEntityResponse InsertTypiconEntity(InsertTypiconEntityRequest insertTypiconEntityRequest);
        UpdateTypiconEntityResponse UpdateTypiconEntity(UpdateTypiconEntityRequest updateTypiconEntityRequest);
        DeleteTypiconEntityResponse DeleteTypiconEntity(DeleteTypiconEntityRequest deleteTypiconEntityRequest);


    }
}
