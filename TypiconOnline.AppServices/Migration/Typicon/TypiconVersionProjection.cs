using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    [Serializable]
    [XmlRoot("typicon")]
    public class TypiconVersionProjection
    {
        public virtual ItemText Name { get; set; }
        public virtual ItemText Description { get; set; }
        public virtual string SystemName { get; set; }
        public virtual string DefaultLanguage { get; set; }
        public bool IsTemplate { get; set; }
        public int OwnerId { get; set; }
        public List<int> Editors { get; set; }
        [XmlArray("Signs")]
        [XmlArrayItem("Sign")]
        public List<SignProjection> Signs { get; set; }
        [XmlArray("CommonRules")]
        [XmlArrayItem("CommonRule")]
        public virtual List<CommonRuleProjection> CommonRules { get; set; }
        [XmlArray("MenologyRules")]
        [XmlArrayItem("MenologyRule")]
        public virtual List<MenologyRuleProjection> MenologyRules { get; set; }
        [XmlArray("TriodionRules")]
        [XmlArrayItem("TriodionRule")]
        public virtual List<TriodionRuleProjection> TriodionRules { get; set; }
        [XmlArray("ExplicitAddRules")]
        [XmlArrayItem("ExplicitAddRule")]
        public virtual List<ExplicitAddRuleProjection> ExplicitAddRules { get; set; }
        [XmlArray("TypiconVariables")]
        [XmlArrayItem("TypiconVariable")]
        public virtual List<TypiconVariableProjection> TypiconVariables { get; set; }
        [XmlArray("Kathismas")]
        [XmlArrayItem("Kathisma")]
        public virtual List<KathismaProjection> Kathismas { get; set; }
        [XmlElement("PrintWeekTemplate")]
        public virtual PrintWeekTemplateProjection PrintWeekTemplate { get; set; }
        [XmlArray("PrintDayTemplates")]
        [XmlArrayItem("PrintDayTemplate")]
        public virtual List<PrintDayTemplateProjection> PrintDayTemplates { get; set; }
        [XmlElement("ScheduleSettings")]
        public virtual ScheduleSettingsProjection ScheduleSettings { get; set; }
    }
}