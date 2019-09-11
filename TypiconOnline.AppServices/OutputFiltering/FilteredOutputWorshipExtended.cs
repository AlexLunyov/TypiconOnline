using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.OutputFiltering
{
    /// <summary>
    /// Фильтрованная и локализованная служба вместе с последовательностью
    /// </summary>
    public class FilteredOutputWorshipExtended : FilteredOutputWorship
    {
        public List<FilteredOutputSection> Sections { get; set; } = new List<FilteredOutputSection>();
    }
}
