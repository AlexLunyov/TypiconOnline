using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Books.Katavasia
{
    public interface IKatavasiaContext
    {
        GetKatavasiaResponse Get(GetKatavasiaRequest request);

        GetAllKatavasiaResponse GetAll();
    }
}
