using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.AppServices.OutputFiltering
{
    public class FilteredOutputDay
    {
        public int Id { get; set; }
        
        public virtual DateTime Date { get; set; }
        
        public virtual FilteredOutputDayHeader Header { get; set; }

        /// <summary>
        /// Коллекция элементов расписания, которые являются контейнерами для последовательностей боослужений
        /// </summary>
        public List<FilteredOutputWorship> Worships { get; set; } = new List<FilteredOutputWorship>();
        public DateTime? ModifiedDate { get; set; }
    }
}
