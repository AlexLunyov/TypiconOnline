using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Rendering
{
    /// <summary>
    /// Абстрактный класс для элементов, которые будут выводиться наружу,
    /// т.е. будут являться конечным результатом обработки правил
    /// </summary>
    public abstract class RenderElement
    {
        public string Text;
    }
}
