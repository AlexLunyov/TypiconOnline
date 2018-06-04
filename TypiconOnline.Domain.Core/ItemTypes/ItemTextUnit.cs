﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Core.ItemTypes
{
    [Serializable]
    public class ItemTextUnit
    {
        public ItemTextUnit() { }

        public ItemTextUnit(ItemTextUnit source)
        {
            if (source == null) throw new ArgumentNullException("ItemTextUnit");

            Text = source.Text;
            Language = source.Language;
        }

        [XmlAttribute("language")]
        public string Language { get; set; }
        [XmlText()]
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
