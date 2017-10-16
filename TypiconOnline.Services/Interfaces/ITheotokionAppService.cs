using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.TheotokionApp;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface ITheotokionAppService : ITheotokionAppContext
    {
        InsertTheotokionResponse InsertTheotokion(InsertTheotokionRequest request);
        UpdateTheotokionResponse UpdateTheotokion(UpdateTheotokionRequest request);
        DeleteTheotokionResponse DeleteTheotokion(DeleteTheotokionRequest request);
    }
}
