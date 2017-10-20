using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Rules
{
    public static class RuleConstants
    {
        public const string ExecContainerNodeName = "rule";

        public const string SwitchNodeName = "switch";
        public const string ExpressionNodeName = "expression";

        public const string ModifyDayNodeName = "modifyday";
        public const string ShortNameAttrName = "shortname";
        public const string IsLastNameAttrName = "islastname";
        public const string AsAdditionAttrName = "asaddition";
        public const string DayMoveAttrName = "daymove";
        public const string PriorityAttrName = "priority";
        public const string UseFullNameAttrName = "usefullname"; 

        public const string DaysFromEasterNodeName = "daysfromeaster";
        public const string DateNodeName = "date";
        public const string IntNodeName = "int";

        public const string GetClosestDayNodeName = "getclosestday";
        public const string DayOfWeekAttrName = "dayofweek";
        public const string WeekCountAttrName = "weekcount";

        public const string CaseNodeName = "case";
        public const string DefaultNodeName = "default";
        public const string ValuesNodeName = "values";
        public const string ValueNodeName = "value";
        public const string ActionNodeName = "action";

        public const string IfNodeName = "if"; 
        public const string ExpressionIfNodeName = "expression";
        public const string ThenNodeName = "then";
        public const string ElseNodeName = "else";

        public const string ServiceNodeName = "service";
        public const string ServiceTimeAttrName = "time";
        public const string ServiceNameAttrName = "name";
        public const string ServiceIsDayBeforeAttrName = "isdaybefore";
        public const string ServiceAdditionalNameAttrName = "additionalname";

        public const string NoticeNodeName = "notice";

        public const string GetDayOfWeekNodeName = "getdayofweek";
        public const string GetDayOfWeekAttrName = "name";

        public const string DateByDaysFromEasterNodeName = "datebydaysfromeaster";

        public const string ModifyReplacedDayNodeName = "modifyreplacedday";
        public const string KindAttrName = "kind";

        public const string ItemTextItemNode = "item";
        public const string ItemTextLanguageAttr = "language";
        public const string StyleNodeName = "style"; 
        public const string StyleRedNodeName = "red";
        public const string StyleBoldNodeName = "bold"; 
        public const string StyleHeaderNodeName = "header";
        public const string ItemTextNoteNode = "note";

        public const string CommonRuleNode = "commonrule";
        public const string CommonRuleNameAttr = "name";

        /*
         * Логические элементы
         */
        public const string EqualsNodeName = "equals";

        public const string MoreNodeName = "more";
        public const string MoreEqualsNodeName = "moreequals";
        public const string LessNodeName = "less";
        public const string LessEqualsNodeName = "lessequals";
        public const string AndNodeName = "and";
        public const string OrNodeName = "or";

        /*
         * Логические элементы, связанные с правилами
         */
        public const string IsCelebratingNodeName = "iscelebrating";
        public const string IsTwoSaintsNodeName = "istwosaints";
        public const string IsExistsNodeName = "exists";


        //правила для распознавания времени и дат
        public const string ItemTimeParsing = "HH.mm";
        public const string ItemDateParsing = "--MM-dd";

        /*
         * Константы для описания xml-схемы богослужебных текстов
         */
        public const string DayElementNode = "dayservice";
        public const string DayElementNameNode = "name";

        public const string MikrosEsperinosNode = "mikrosesperinos"; 
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
        public const string TheotokionKindAttrValue = "theotokion";
        public const string TriadikoKindAttrValue = "triadiko";
        public const string MartyrionKindAttrValue = "martyrion";

        public const string KanonasSedalenNode = "sedalen"; 
        public const string KanonasKontakionNode = "kontakion";
        public const string IkosNode = "ikos";
        public const string KanonasExapostilarionNode = "exapostilarion";
        public const string KanonasExapostilarionYmnosNode = "ymnos";

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

        /*
         * Константы для описания правил для составления последовательностей богослужебных текстов
         */

        public const string KekragariaRuleNode = "kekragaria";
        public const string YmnosStructureRuleNode = "kekragaria";
        public const string ShowPsalmAttribute = "showpsalm";
        public const string TotalCountAttribute = "totalcount";

        /*
         * Константы для описания атрибутов элемента ymnosRule
        */
        public const string YmnosRuleNode = "ymnosrule";
        public const string YmnosRuleDoxastichonNode = "doxastichonrule";
        public const string YmnosRuleTheotokionNode = "theotokionrule";
        public const string YmnosRuleSourceAttrName = "source";
        public const string YmnosRulePlaceAttrName = "place";
        public const string YmnosRuleCountAttrName = "count";
        public const string YmnosRuleStartFromAttrName = "startfrom";

        /*
         * TextHolder
         */
        public const string TextHolderPapragraphNode = "p";
        public const string TextHolderLectorNode = "lector";
        public const string TextHolderChoirNode = "choir";
        public const string TextHolderDeaconNode = "deacon";
        public const string TextHolderPriestNode = "priest";
        public const string TextHolderTextNode = "text";

        public const string TextHolderMarkAttr = "mark";

        /*
         * Ektenis
         */
        public const string EktenisNode = "ektenis";
        public const string EktenisNameNode = "name";

        public enum DefinitionsDayOfWeek { понедельник=1, вторник=2, среда=3, четверг=4, пятница=5, суббота=6, воскресенье=7 };

        public enum KindOfReplacedDay { undefined=0, menology=1, triodion=2 };
    }

    /// <summary>
    /// Перечисление источников (богослуебных книг) для текстов богослужения
    /// </summary>
    public enum YmnosSource { Oktoikh = 0, Item1 = 1, Item2 = 2, Item3 = 3, Irmologion = 4 }

    /// <summary>
    /// Перечисление мест в богослужебных книгах для текстов богослужения
    /// </summary>
    public enum PlaceYmnosSource
    {
        //kekragaria
        kekragaria = 0,
        kekragaria_doxastichon = 1,
        kekragaria_theotokion = 2,
        kekragaria_stavrostheotokion = 22,
        //liti
        liti = 3,
        liti_doxastichon = 4,
        liti_theotokion = 5,
        //aposticha_esperinos
        aposticha_esperinos = 6,
        aposticha_esperinos_doxastichon = 7,
        aposticha_esperinos_theotokion = 8,
        //ainoi
        ainoi = 9,
        ainoi_doxastichon = 10,
        ainoi_theotokion = 11,
        //aposticha_orthros
        aposticha_orthros = 12,
        aposticha_orthros_doxastichon = 13,
        aposticha_orthros_theotokion = 14,
        //Irmologion
        app1_kekragaria = 15,
        app1_aposticha = 16,
        app2_esperinos = 17,
        app2_orthros = 18,
        app3 = 19,
        app4_esperinos = 20,
        app4_orthros = 21,
        //troparion
        troparion = 23
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
        Nekrosimo
    }

    /// <summary>
    /// Разновидность песнопений (используется при составлении ПРАВИЛ)
    /// </summary>
    public enum YmnosRuleKind {
        [XmlEnum(Name = RuleConstants.YmnosKindAttrValue)]
        YmnosRule,
        DoxastichonRule,
        TheotokionRule
    }
    
    public enum YmnosStructureKind { Kekragaria, Aposticha, Liti, Ainoi }

    public enum ServiceSequenceKind { MikrosEsperinos, Esperinos, Orthros, Hour, Leitourgia }

    public enum TextHolderKind { Choir, Lector, Priest, Deacon, Stihos, Text, Undefined }

    /// <summary>
    /// Пометка текста определенным знаком.
    /// В настройках вывода последовательности возможно будет не отображать элемент с определенными пометками
    /// </summary>
    public enum TextHolderMark
    {
        /// <summary>
        /// священические молитвы
        /// </summary>
        priest_prayers
    }

    [Serializable]
    public enum ProkiemenonKind
    {
        [XmlEnum(Name = RuleConstants.ProkeimenonNode)]
        Prokiemenon,
        [XmlEnum(Name = RuleConstants.ProkeimenonKindAlleluia)]
        Alleluia
    }
    [Serializable]
    public enum Languages
    {
        [XmlEnum(Name = "cs-cs")]
        cs_cs,
        [XmlEnum(Name = "cs-ru")]
        cs_ru,
        [XmlEnum(Name = "ru-ru")]
        ru_ru,
        [XmlEnum(Name = "el-el")]
        el_el
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

    public enum State { Valid = 0, Invalid = 1, Undefined = 2}

    /// <summary>
    /// Используется при обработке правил
    /// </summary>
    public enum HandlingMode
    {
        All = 0,
        ThisDay = 1,
        DayBefore = 2,
        AstronimicDay = 3
    }
}
