﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Extensions
{
    static class XmlNodeExtensions
    {
        public static ItemTextUnit GetItemTextUnit(this XmlNode elem, string key)
        {
            var node = elem.SelectSingleNode(key);

            if (node != null)
            {
                var attr = node.Attributes[RuleConstants.ItemTextLanguageAttr];

                var language = (attr != null) ? attr.Value : string.Empty;

                return new ItemTextUnit()
                {
                    Text = node.InnerText,
                    Language = language
                };
            }

            return default(ItemTextUnit);
        }

        public static ItemTextStyled GetItemTextStyled(this XmlNode elem, string key)
        {
            var node = elem.SelectSingleNode(key);

            return (node != null) ? new ItemTextStyled(node.OuterXml, key) : default(ItemTextStyled);
        }
    }
}
