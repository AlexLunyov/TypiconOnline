using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.Typicon.Variable
{
    /// <summary>
    /// Список возможных типов для значений Переменных Устава
    /// </summary>
    public enum VariableType
    {
        Date = 0,
        Time = 1,
        Integer = 2,
        String = 3,
        //Многоязыковая строка
        ItemText = 4,
        //Описание службы
        Worship = 5
    }
}
