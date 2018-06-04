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
        public const string ShortNameNodeName = "shortname";
        public const string IsLastNameAttrName = "islastname";
        public const string AsAdditionAttrName = "asaddition";
        public const string DayMoveAttrName = "daymove";
        public const string PriorityAttrName = "priority";
        public const string UseFullNameAttrName = "usefullname";
        public const string SignNumberAttrName = "signnumber"; 
        public const string ModifyDayIdAttrName = "id";
        public const string ModifyDayAsadditionModeAttrName = "asadditionmode";

        /*
         * DayWorshipsFilter
         */
        public const string FilterExcludeItemAttr = "excludeitem";
        public const string FilterIncludeItemAttr = "includeitem";
        public const string FilterIsCelebratingAttr = "iscelebrating";

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

        /*
         * Константы для описания атрибутов элемента WorshipRule
         */

        public const string WorshipRuleNodeName = "worship";
        public const string WorshipRuleIdAttrName = "id";
        public const string WorshipRuleTimeAttrName = "time";
        public const string WorshipRuleNameNode = "name";
        public const string WorshipRuleIsDayBeforeAttrName = "isdaybefore";
        public const string WorshipRuleAdditionalNameNode = "additionalname";
        public const string WorshipRuleSequenceNode = "sequence";

        public const string NoticeNodeName = "notice";

        public const string GetDayOfWeekNodeName = "getdayofweek";
        public const string GetDayOfWeekAttrName = "name";

        public const string DateByDaysFromEasterNodeName = "datebydaysfromeaster";

        public const string ModifyReplacedDayNodeName = "modifyreplacedday";
        public const string KindAttrName = "kind";

        

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

        /*
         * Константы для описания атрибутов элементов IAsAdditionElement
        */
        public const string IAsAdditionElementModeAttrName = "asaddition";
        public const string IAsAdditionElementRewriteAttrName = "rewrite";
        public const string IAsAdditionElementAppendAttrName = "append";

        /*
         * Константы для описания атрибутов элемента KanonasRule
        */
        public const string KanonasRuleNode = "kanonasrule";
        public const string KanonasRulePanagiasAttrName = "panagias";
        public const string IsOrthrosAttribute = "isorthros";

        /*
         * Константы для описания атрибутов элемента KanonasItem
        */
        public const string KanonasItemNode = "k_kanonas";
        public const string KanonasSourceAttrName = "source";
        public const string KanonasKindAttrName = "kanonas";
        public const string KanonasCountAttrName = "count"; 
        public const string KanonasMartyrionAttrName = "martyrion";
        public const string KanonasIrmosCountAttrName = "irmoscount";

        /*
         * Константы для описания атрибутов элемента KKontakionRule
        */
        public const string KontakionRuleNode = "kontakionrule";
        public const string KontakionPlaceAttrName = "place";
        public const string KontakionShowIkosAttrName = "showikos";

        /*
         * Константы для описания атрибутов элемента KSedalenRule
        */
        public const string KSedalenNode = "k_sedalen";
        public const string KSedalenPlaceAttrName = "place";

        /*
         * Константы для описания атрибутов элемента KSedalenTheotokionRule
        */
        public const string KSedalenTheotokionNode = "k_sedalentheotokion";

        /*
         * Константы для описания атрибутов элемента KSedalenTheotokionRule
        */
        public const string KKatavasiaNode = "k_katavasia";
        public const string KKatavasiaNameAttr = "name";

        /*
         * Константы для описания атрибутов элемента KAfterRule
        */
        public const string KAfterNode = "k_after";
        public const string KAfterOdiNumberAttrName = "number";

        /*
         * Константы для описания атрибутов элемента KOdiRule
        */
        public const string KOdiRuleNode = "k_odi";
        public const string KOdiRuleNumberAttrName = "number";

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

        /*
         * Константы для описания элемента чтения Священного Писания
         */
        public const string PsalmStihosNode = "psalmstihos";
        public const string ReadingChapterNumberAttr = "chapter";
        public const string ReadingStihosNumberAttr = "number";

        public const string BookReadingAnnotationNode = "annotation";
        public const string BookReadingTextNode = "text";
        public const string BookReadingStihosNode = "stihos";

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
        public const string YmnosRuleKindAttrName = "kind";
        public const string YmnosRuleSourceAttrName = "source";
        public const string YmnosRulePlaceAttrName = "place";
        public const string YmnosRuleCountAttrName = "count";
        public const string YmnosRuleStartFromAttrName = "startfrom";

        /*
         * Константы для описания атрибутов элемента TheotokionAppRule
        */
        public const string TheotokionAppRuleNode = "theotokionapprule";
        public const string TheotokionAppRulePlaceAttrName = "place";

        /*
         * Константы для описания атрибутов элемента YmnosCustomRule
        */
        public const string YmnosCustomRuleNode = "ymnoscustomrule";
        public const string YmnosCustomRuleGroupNode = "group";
        public const string YmnosCustomRuleDoxastichonNode = "doxastichon";
        public const string YmnosCustomRuleTheotokionNode = "theotokion";

        /*
         * Константы для описания атрибутов элемента ExapostilarionRule
        */
        public const string ExapostilarionRuleNode = "exapostilarionrule";

        /*
         * Константы для описания атрибутов элемента ExapostilarionItemRule
        */
        public const string ExapostilarionItemRuleNode = "exap_item";
        public const string ExapostilarionItemRulePlaceAttrName = "place";
        public const string ExapostilarionItemRuleKindAttrName = "kind";
        public const string ExapostilarionItemRuleCountAttrName = "count";

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

        /*
         * PsalmRule
         */
        public const string PsalmRuleNode = "psalm";
        public const string PsalmRuleNumberAttr = "number";
        public const string PsalmRuleStartAttr = "start";
        public const string PsalmRuleEndAttr = "end";

        

        
    }

    /// <summary>
    /// Используется в классе ModifyReplacedDay
    /// </summary>
    public enum KindOfReplacedDay { Menology, Triodion };

    /// <summary>
    /// Перечисление источников (богослужебных книг) для текстов богослужения
    /// </summary>
    public enum YmnosSource { Oktoikh = 0, Item1 = 1, Item2 = 2, Item3 = 3, WeekDay = 4 }

    /// <summary>
    /// Перечисление источников (богослужебных книг) для канонов
    /// </summary>
    public enum KanonasSource { Oktoikh = 0, Item1 = 1, Item2 = 2, Item3 = 3}

    /// <summary>
    /// Перечисление мест в богослужебных книгах для текстов богослужения
    /// </summary>
    public enum PlaceYmnosSource
    {
        //kekragaria
        kekragaria,
        kekragaria_doxastichon,
        kekragaria_theotokion,
        kekragaria_stavrostheotokion,
        //liti
        liti,
        liti_doxastichon,
        liti_theotokion,
        //aposticha_esperinos
        aposticha_esperinos,
        aposticha_esperinos_doxastichon,
        aposticha_esperinos_theotokion,
        //ainoi
        ainoi,
        ainoi_doxastichon,
        ainoi_theotokion,
        //aposticha_orthros
        aposticha_orthros,
        aposticha_orthros_doxastichon,
        aposticha_orthros_theotokion,
        //troparion
        troparion,
        troparion_doxastichon,
        troparion_theotokion,
        //Sedalen
        sedalen1,
        sedalen1_doxastichon,
        sedalen1_theotokion,
        sedalen2,
        sedalen2_doxastichon,
        sedalen2_theotokion,
        sedalen3,
        sedalen3_doxastichon,
        sedalen3_theotokion,
        sedalen_kanonas,
        sedalen_kanonas_theotokion,
        sedalen_kanonas_stavrostheotokion,
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
    /// Разновидность песнопений (используется при составлении ПРАВИЛ)
    /// </summary>
    public enum YmnosRuleKind {
        [XmlEnum(Name = RuleConstants.YmnosKindAttrValue)]
        Ymnos,
        [XmlEnum(Name = RuleConstants.DoxastichonKindAttrValue)]
        Doxastichon,
        [XmlEnum(Name = RuleConstants.TheotokionKindAttrValue)]
        Theotokion
    }
    
    public enum YmnosStructureKind { Kekragaria, Aposticha, Liti, Ainoi }

    public enum WorshipSequenceKind { MikrosEsperinos, Esperinos, MegalisEsperinos, MikrosApodipno, MegalisApodipno, Mesoniktiko, Orthros, Hour, Leitourgia }

    public enum TextHolderKind { Choir, Lector, Priest, Deacon, Stihos, Text, Undefined }

    /// <summary>
    /// Разновидность песнопений элемента последовательности эксапостилария 
    /// (используется при составлении ПРАВИЛ)
    /// </summary>
    public enum ExapostilarionItemKind { Exap, Theotokion }

    /// <summary>
    /// Разновидность песнопений элемента последовательности эксапостилария 
    /// (используется при составлении ПРАВИЛ)
    /// </summary>
    public enum ExapostilarionItemPlace { Exap1, Exap2, Theotokion }

    /// <summary>
    /// Опредения места канона в богослужебном тексте
    /// </summary>
    public enum KanonasKind
    {
        Apodipno,
        Mesoniktiko,
        Orthros1,
        Orthros2,
        Orthros3
    }

    /// <summary>
    /// Опредения места в каноне в богослужебном тексте
    /// </summary>
    public enum KanonasPlaceKind
    {
        sedalen,
        sedalen_theotokion,
        sedalen_stavrostheotokion,
        kontakion
    }

    /// <summary>
    /// Определение места кондака в тексте богослужения
    /// </summary>
    public enum KontakionPlace
    {
        orthros1,
        orthros2
    }

    /// <summary>
    /// Разновидность библейских канона утрени
    /// </summary>
    public enum BibleOdiKind
    {
        Undefined,
        Poem_Gospodevi,
        Gospodevi_poem
    }

    /// <summary>
    /// Пометка текста определенным знаком.
    /// В настройках вывода последовательности возможно будет не отображать элемент с определенными пометками
    /// </summary>
    public enum TextHolderMark
    {
        undefined,
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
        AstronomicDay = 3
    }
}
