using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Books.Irmologion
{
    public interface IReadOnlyIrmTheotokionService
    {
        GetTheotokionResponse GetTheotokion(GetTheotokionRequest request);

        GetAllTheotokionResponse GetAll();
    }
}
