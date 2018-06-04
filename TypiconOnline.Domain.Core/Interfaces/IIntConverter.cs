using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Core.Interfaces
{
    /// <summary>
    /// Интерфейс 
    /// </summary>
    public interface IIntConverter
    {
        int Parse(string str);
        bool TryParse(string str, out int value);
        bool IsDigit(string str);
        string ToString(int value);
    }
}
