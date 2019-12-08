using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class PrintWeekTemplateModel
    {
        public int Id { get; set; }

        public int TypiconVersionId { get; set; }

        public int DaysPerPage { get; set; }

        public bool HasFile { get; set; }
        /// <summary>
        /// Имя загруженного файла
        /// </summary>
        public string PrintFileName { get; set; }
    }
}
