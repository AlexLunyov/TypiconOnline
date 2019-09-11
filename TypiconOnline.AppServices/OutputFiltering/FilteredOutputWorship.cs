﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.OutputFiltering
{
    public class FilteredOutputWorship
    {
        /// <summary>
        /// Id из Базы данных
        /// </summary>
        public int Id { get; set; }
        public string Time { get; set; }
        public FilteredParagraph Name { get; set; }
        public ItemTextUnit AdditionalName { get; set; }
        public bool HasSequence { get; set; }
    }
}
