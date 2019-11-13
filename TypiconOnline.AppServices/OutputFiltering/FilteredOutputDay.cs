﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.OutputFiltering
{
    public class FilteredOutputDay
    {
        public ItemTextUnit Name { get; set; }
        public virtual DateTime Date { get; set; }
        /// <summary>
        /// Номер знака службы
        /// </summary>
        public int SignNumber { get; set; }
        /// <summary>
        /// Наименование знака службы
        /// </summary>
        public ItemTextUnit SignName { get; set; }

        /// <summary>
        /// Коллекция элементов расписания, которые являются контейнерами для последовательностей боослужений
        /// </summary>
        public List<FilteredOutputWorship> Worships { get; set; } = new List<FilteredOutputWorship>();
    }
}