using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IXmlSerializer
    {
        T Deserialize<T>(string xml) where T : class;
        string Serialize<T>(T value) where T : class;
    }
}
