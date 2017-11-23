using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс, инкапсулирующий получение имени элемента правил
    /// </summary>
    public interface IDescriptor
    {
        string Description { get; set; }
        string GetElementName();
    }
}
