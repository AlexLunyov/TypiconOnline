﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Core.ItemTypes
{
    /// <summary>
    /// Определение стиля для элементов правил
    /// </summary>
    [Serializable]
    public class TextStyle
    {
        [XmlElement(CommonConstants.StyleRedNodeName)]
        public bool IsRed { get; set; } = false;
        [XmlElement(CommonConstants.StyleBoldNodeName)]
        public bool IsBold { get; set; } = false;
        [XmlElement(CommonConstants.StyleItalicNodeName)]
        public bool IsItalic { get; set; } = false;
        [XmlElement(CommonConstants.StyleHeaderNodeName)]
        public HeaderCaption Header { get; set; } = HeaderCaption.NotDefined;
    }

    [Serializable]
    public enum HeaderCaption
    {
        [XmlEnum(Name = "notdefined")]
        NotDefined = 0,
        [XmlEnum(Name = "h1")]
        h1 = 1,
        [XmlEnum(Name = "h2")]
        h2 = 2,
        [XmlEnum(Name = "h3")]
        h3 = 3,
        [XmlEnum(Name = "h4")]
        h4 = 4
    }
}
