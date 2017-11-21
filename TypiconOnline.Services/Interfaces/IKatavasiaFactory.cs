using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Katavasia;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IKatavasiaFactory
    {
        Katavasia Create(string name, string definition);
    }
}
