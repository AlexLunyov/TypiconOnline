using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Typicon;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface ITypiconVersionService
    {
        void ClearModifiedYears(int id);
        void ReloadRules(int id, string folderPath);
        GetTypiconVersionResponse GetTypiconVersion(int id);
        GetTypiconEntitiesResponse GetAllTypiconEntities();
        InsertTypiconVersionResponse InsertTypiconVersion(InsertTypiconVersionRequest insertTypiconVersionRequest);
        UpdateTypiconVersionResponse UpdateTypiconVersion(UpdateTypiconVersionRequest updateTypiconVersionRequest);
        DeleteTypiconVersionResponse DeleteTypiconVersion(DeleteTypiconVersionRequest deleteTypiconVersionRequest);


    }
}
