using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class VariableEditTimeModel: VariableEditModelBase
    {
        /// <summary>
        /// Значение переменной в формате, зависимом от типа переменной
        /// </summary>
        public string Value { get; set; }
    }
}
