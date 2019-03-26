using System;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Books.Elements
{
    public static class ElementConstants
    {
        /*
         * Константы для описания xml-схемы богослужебных текстов
         */

        public const string DayElementNode = "dayservice";
        public const string DayElementNameNode = "name";

        public const string MikrosEsperinosNode = "mikrosesperinos";
        public const string MegalisEsperinosNode = "megalisesperinos";
        public const string EsperinosNode = "esperinos";
        public const string OrthrosNode = "orthros";
        public const string SixHourNode = "sixhour";
        public const string LeitourgiaNode = "leitourgia";

        public const string KekragariaNode = "kekragaria";

        public const string IhosAttrName = "ihos";
        public const string YmnosGroupKindAttrName = "kind";

        public const string ProkeimeniNode = "prokeimeni";
        public const string ProkeimenonNode = "prokeimenon";
        public const string ProkeimenonKindAttr = "kind";
        public const string ProkeimenonKindAlleluia = "alleluia";

        public const string ParoimiesNode = "paroimies";
        public const string ParoimiaNode = "paroimia";
        public const string ParoimiaQuoteAttr = "quote";
        public const string ParoimiaBookNameNode = "bookname";
        public const string ParoimiaStihosNode = "stihos";

        public const string ItemTextCollectionNode = "item";

        public const string ApostichaNode = "aposticha";
        public const string LitiNode = "liti";
        public const string TroparionNode = "troparion";
        public const string SedalenNode = "sedalen";

        public const string SedalenKathisma1Node = "sed_1kathisma";
        public const string SedalenKathisma2Node = "sed_2kathisma";
        public const string SedalenKathisma3Node = "sed_3kathisma";
        public const string SedalenPolyeleosNode = "sed_polyeleos";
        public const string AnavathmosNode = "anavathmos";
        public const string MegalynarionNode = "megalynarion";
        public const string EclogarionNode = "eclogarion";

        public const string EvangelionNode = "evangelion";
        public const string EvangelionPartNode = "part";
        public const string EvangelionBookNameAttr = "bookname";
        public const string EvangelionPartNumberAttr = "number";

        public const string EvangelionBokMf = "Мф";
        public const string EvangelionBokMk = "Мк";
        public const string EvangelionBokLk = "Лк";
        public const string EvangelionBokIn = "Ин";

        public const string Sticheron50Node = "sticheron_50";
        public const string AinoiNode = "ainoi";

        public const string KanonesNode = "kanones";
        public const string KanonasNode = "kanonas";
        public const string KanonasAcrosticNode = "acrostic";
        public const string KanonasNameNode = "name";
        public const string KanonasAnnotationNode = "annotation";
        public const string KanonasStihosNode = "stihos";

        public const string KanonasOdesNode = "odes";
        public const string KanonasOdiNode = "odi";
        public const string OdiNumberAttrName = "number";
        public const string OdiIrmosNode = "irmos";
        public const string OdiKatavasiaNode = "katavasia";
        public const string OdiTroparionName = "troparion";
        public const string OdiTroparionKindAttr = "kind";

        public const string YmnosKindAttrValue = "ymnos";
        public const string DoxastichonKindAttrValue = "doxastichon";
        public const string TheotokionKindAttrValue = "theotokion";
        public const string TriadikoKindAttrValue = "triadiko";
        public const string MartyrionKindAttrValue = "martyrion";

        public const string KanonasSedalenNode = "sedalen_kanonas";
        public const string KontakiaNode = "kontakia";
        public const string KontakionNode = "kontakion";
        public const string IkosNode = "ikos";
        public const string ExapostilarionNode = "exapostilarion";
        public const string ExapostilarionYmnosNode = "ymnos";
        public const string ExapostilarionTheotokionNode = "theotokion";

        public const string YmnosStructureGroupsNode = "groups";
        public const string YmnosStructureGroupNode = "group";
        public const string YmnosStructureDoxastichonNode = "doxastichon";
        public const string YmnosStructureTheotokionNode = "theotokion";

        public const string ProsomoionNode = "prosomoion";
        public const string ProsomoionSelfAttr = "self";

        public const string AnnotationNode = "annotation";

        public const string YmnisNode = "ymnis";

        public const string YmnosNode = "ymnos";
        public const string YmnosIhosAttrName = "ihos";
        public const string YmnosTextNode = "text";
        public const string YmnosStihosNode = "stihos";

        public const string SixHourTroparionNode = "troparion";

        public const string MakarismiNode = "makarismi";
        public const string MakarismiOdiNode = "odi";
        public const string MakarismiOdiNumberAttr = "number";
        public const string MakarismiOdiCountAttr = "count";
        public const string MakarismiYmnisNode = "ymnis";

        public const string ApostlesNode = "apostles";
        public const string AlleluiaNode = "alleluia";
        public const string KinonikNode = "kinonik";

        public const string BookReadingAnnotationNode = "annotation";
        public const string BookReadingTextNode = "text";
        public const string BookReadingStihosNode = "stihos";

        /*
         * Константы для описания элемента чтения Священного Писания
         */
        public const string PsalmStihosNode = "psalmstihos";
        public const string ReadingChapterNumberAttr = "chapter";
        public const string ReadingStihosNumberAttr = "number";

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

        //правила для распознавания времени и дат
        public const string ItemTimeParsing = "HH.mm";
        public const string ItemDateParsing = "--MM-dd";
    }

    /// <summary>
    /// Перечисление мест в богослужебных книгах для богородичных приложений из Ирмология
    /// </summary>
    public enum TheotokionAppPlace
    {
        app1_kekragaria,
        app1_aposticha,
        app2_esperinos,
        app2_orthros,
        app3,
        app4_esperinos,
        app4_orthros
    }

    /// <summary>
    /// Разновидность песнопений (используется при составлении ПРАВИЛ)
    /// </summary>
    public enum YmnosKind
    {
        /// <summary>
        /// общий, по умолчанию
        /// </summary>
        [XmlEnum(Name = "ymnos")]
        Ymnos,
        /// <summary>
        /// богородичен
        /// </summary>
        [XmlEnum(Name = "theotokion")]
        Theotokion,
        /// <summary>
        /// троичен
        /// </summary>
        [XmlEnum(Name = "triadiko")]
        Triadiko,
        /// <summary>
        /// мученикам
        /// </summary>
        [XmlEnum(Name = "martyrion")]
        Martyrion,
        /// <summary>
        /// святителям
        /// </summary>
        [XmlEnum(Name = "ierarhon")]
        Ierarhon,
        /// <summary>
        /// преподобным
        /// </summary>
        [XmlEnum(Name = "osion")]
        Osion,
        /// <summary>
        /// заупокойный
        /// </summary>
        [XmlEnum(Name = "nekrosimo")]
        Nekrosimo,
        /// <summary>
        /// ирмос у песни канона
        /// </summary>
        [XmlEnum(Name = "irmos")]
        Irmos,
        /// <summary>
        /// катавасия у песни канона
        /// </summary>
        [XmlEnum(Name = "katavasia")]
        Katavasia
    }

    /// <summary>
    /// Разновидность группы песнопений
    /// </summary>
    [Serializable]
    public enum YmnosGroupKind
    {
        [XmlEnum(Name = "undefined")]
        Undefined,
        [XmlEnum(Name = "stavros")]
        Stavros
    }
}
