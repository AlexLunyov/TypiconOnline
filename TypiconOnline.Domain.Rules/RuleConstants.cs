using System;
using System.Xml.Serialization;
using TypiconOnline.Domain.Books.Elements;

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

        /*
         * Константы для описания правил для составления последовательностей богослужебных текстов
         */

        public const string KekragariaRuleNode = "kekragaria";
        public const string YmnosStructureRuleNode = "kekragaria";
        public const string ShowPsalmAttribute = "showpsalm";
        public const string TotalCountAttribute = "totalcount";

        public const string AinoiRuleNode = "ainoi";
        public const string ApostichaRuleNode = "aposticha";
        public const string LitiRuleNode = "liti";

        public const string SedalenRuleNode = "sedalen";
        public const string TroparionRuleNode = "troparion";

        public const string MikrosEsperinosNode = "mikrosesperinos";
        public const string MegalisEsperinosNode = "megalisesperinos";
        public const string EsperinosNode = "esperinos";
        public const string OrthrosNode = "orthros";
        public const string SixHourNode = "sixhour";
        public const string LeitourgiaNode = "leitourgia";

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
    /// Разновидность песнопений (используется при составлении ПРАВИЛ)
    /// </summary>
    public enum YmnosRuleKind {
        [XmlEnum(Name = ElementConstants.YmnosKindAttrValue)]
        Ymnos,
        [XmlEnum(Name = ElementConstants.DoxastichonKindAttrValue)]
        Doxastichon,
        [XmlEnum(Name = ElementConstants.TheotokionKindAttrValue)]
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
        [XmlEnum(Name = ElementConstants.ProkeimenonNode)]
        Prokiemenon,
        [XmlEnum(Name = ElementConstants.ProkeimenonKindAlleluia)]
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

    

    public enum State { Valid = 0, Invalid = 1, Undefined = 2}

    /// <summary>
    /// Используется при обработке правил
    /// </summary>
    public enum HandlingMode
    {
        /// <summary>
        /// Все подряд
        /// </summary>
        All = 0,
        /// <summary>
        /// DayBefore будет исключен
        /// </summary>
        ThisDay = 1,
        /// <summary>
        /// ThisDay будет исключен
        /// </summary>
        DayBefore = 2,
        /// <summary>
        /// ThisDay + DayBefore следующего дня
        /// </summary>
        AstronomicDay = 3
    }
}
