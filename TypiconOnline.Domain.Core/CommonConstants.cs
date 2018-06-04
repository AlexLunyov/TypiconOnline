using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Core
{
    public static class CommonConstants
    {
        //правила для распознавания времени и дат
        public const string ItemTimeParsing = "HH.mm";
        public const string ItemDateParsing = "--MM-dd";

        /*
         * Константы для описания атрибутов элемента ItemText
         */
        public const string ItemTextDefaultNode = "text";
        public const string ItemTextItemNode = "item";
        public const string ItemTextLanguageAttr = "language";
        public const string StyleNodeName = "style";
        public const string StyleRedNodeName = "red";
        public const string StyleBoldNodeName = "bold";
        public const string StyleItalicNodeName = "italic";
        public const string StyleHeaderNodeName = "header";
        public const string ItemTextNoteNode = "note";
    }

    public enum DefinitionsDayOfWeek { понедельник = 1, вторник = 2, среда = 3, четверг = 4, пятница = 5, суббота = 6, воскресенье = 7 };
}
