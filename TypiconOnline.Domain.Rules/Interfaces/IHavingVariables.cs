using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.Rules.Interfaces
{
    /// <summary>
    /// Реализуется теми Элементами Правил, где могут быть опеределены Переменные Устава
    /// </summary>
    public interface IHavingVariables
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Имя переменной и ее тип</returns>
        IEnumerable<(string Name, string Type)> GetVariableNames();
    }
}
