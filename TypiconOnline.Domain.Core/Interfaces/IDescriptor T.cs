using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Core.Interfaces
{
    /// <summary>
    /// Интерфейс, инкапсулирующий получение имени элемента правил
    /// </summary>
    public interface IDescriptor<T> : IDescriptor where T: class
    {
        T Element { get; set; }
    }
}
