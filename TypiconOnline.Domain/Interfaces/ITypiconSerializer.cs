using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Interfaces
{
    public interface ITypiconSerializer
    {
        T Deserialize<T>(string expression) where T : class;
        T Deserialize<T>(string expression, string rootElement) where T : class;
        string Serialize<T>(T value) where T : class;
        string Serialize<T>(T value, string rootElement) where T : class;
    }
}
