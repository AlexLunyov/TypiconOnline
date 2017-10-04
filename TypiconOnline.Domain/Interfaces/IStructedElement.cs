using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для элементов правил последовательностей, которые могут быть заменены в Добавлениях (Additions)
    /// </summary>
    public interface IStructedElement
    {
        string StructureName { get; set; }
    }
}
