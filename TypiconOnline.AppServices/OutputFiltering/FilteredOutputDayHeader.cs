using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.AppServices.OutputFiltering
{
    public class FilteredOutputDayHeader
    {
        public ItemTextUnit Name { get; set; }

        public int SignNumber { get; set; }
        /// <summary>
        /// Символ знака службы
        /// </summary>
        public int? Icon { get; set; }
        /// <summary>
        /// Отображать ли красным наименование дня
        /// </summary>
        public bool IsRed { get; set; }
        /// <summary>
        /// Наименование знака службы
        /// </summary>
        public string SignName { get; set; }
    }
}
