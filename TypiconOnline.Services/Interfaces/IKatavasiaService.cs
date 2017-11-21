using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.Katavasia;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IKatavasiaService : IKatavasiaContext
    {
        InsertKatavasiaResponse Insert(InsertKatavasiaRequest request);
        UpdateKatavasiaResponse Update(UpdateKatavasiaRequest request);
        DeleteKatavasiaResponse Delete(DeleteKatavasiaRequest request);
    }
}
