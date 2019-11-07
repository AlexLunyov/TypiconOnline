using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon.Variable;

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
        IEnumerable<(string Name, VariableType Type)> GetVariableNames();
    }
}
