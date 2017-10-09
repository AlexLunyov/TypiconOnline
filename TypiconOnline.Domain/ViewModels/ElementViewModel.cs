using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.ViewModels
{
    /// <summary>
    /// Абстрактный класс для элементов, которые будут выводиться наружу,
    /// т.е. будут являться конечным результатом обработки правил
    /// </summary>
    public abstract class ElementViewModel
    {
        public string Text { get; set; }

    }
}
