using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TypiconOnline.Domain.Rules
{
    public static class RuleConstants
    {
        public const string ExecContainerNodeName = "rule";

        public const string SwitchNodeName = "switch";
        public const string ConditionNodeName = "condition";

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

        public const string StyleNodeName = "style";
        public const string StyleRedNodeName = "red";
        public const string StyleBoldNodeName = "bold";

        //правила для распознавания времени и дат
        public const string ItemTimeParsing = "HH.mm";
        public const string ItemDateParsing = "--MM-dd";

        //public const string ExecContainerNodeName = "правило";
        //public const string SwitchNodeName = "выбор";
        //public const string ConditionNodeName = "условие";
        //public const string ModifyDayNodeName = "деньизменение";
        //public const string ServiceSignAttrName = "знакслужбы";
        //public const string IsCustomNameAttrName = "своеимя";
        //public const string DayMoveAttrName = "деньсмещение";
        //public const string DaysFromEasterNodeName = "днейотпасхи";
        //public const string DateNodeName = "дата";
        //public const string IntNodeName = "число";
        //public const string GetClosestDayNodeName = "ближайшийдень";
        //public const string DayOfWeekAttrName = "деньнедели";
        //public const string WeekCountAttrName = "количествонедель";
        //public const string CaseNodeName = "вариант";
        //public const string DefaultNodeName = "поумолчанию";
        //public const string ValuesNodeName = "значения";
        //public const string ValueNodeName = "значение";
        //public const string ActionNodeName = "action";

        public enum DefinitionsDayOfWeek { понедельник=1, вторник=2, среда=3, четверг=4, пятница=5, суббота=6, воскресенье=7 };

        public enum KindOfReplacedDay { undefined=0, menology=1, triodion=2 };
    }

    public enum State { Valid = 0, Invalid = 1, NotDefined = 2}
}
