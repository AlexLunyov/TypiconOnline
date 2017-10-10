using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Books.Irmologion
{
    public interface IIrmologionService
    {
        GetTheotokionResponse GetTheotokion(GetTheotokionRequest request);
    }
}
