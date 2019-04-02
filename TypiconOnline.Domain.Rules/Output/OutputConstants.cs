namespace TypiconOnline.Domain.Rules.Output
{
    /// <summary>
    /// Константы для выходных форм последовательностей богослужения
    /// </summary>
    public static class OutputConstants
    {
        /*
         * Константы для элементов класса ScheduleWeek
        */
        public const string OutputWeekNode = "week";
        public const string OutputWeekNameNode = "name";

        /*
         * Константы для элементов класса OutputDay
        */
        public const string OutputDayNode = "day";
        public const string OutputDayNameNode = "name";
        public const string OutputDayDateNode = "date";
        public const string OutputDaySignNumberNode = "signnumber";
        public const string OutputDaySignNameNode = "signname";

        /*
         * Константы для элементов класса OutputWorship
        */
        public const string OutputWorshipNodeName = "worship"; 
        public const string OutputWorshipIdAttrName = "id"; 
        public const string OutputWorshipTimeAttrName = "time";
        public const string OutputWorshipNameAttrName = "name"; 
        public const string OutputWorshipAdditionalNameAttrName = "additionalname"; 
        public const string OutputWorshipChildNodeName = "sections";

        /*
         * Константы для элементов класса OutputSection
        */
        public const string OutputSectionNodeName = "section";
        public const string OutputSectionKindTextNodeName = "kindtext"; 
        public const string OutputSectionKindAttrName = "kind"; 
        public const string OutputSectionChildNodeName = "paragraphs";

        /*
         * Константы для элементов класса LocalizedOutputWeek
        */
        public const string LocalizedOutputWeekNode = "week";
        public const string LocalizedOutputWeekNameNode = "name";

        /*
         * Константы для элементов класса LocalizedOutputDay
        */
        public const string LocalizedOutputDayNode = "day";
        public const string LocalizedOutputDayNameNode = "name";
        public const string LocalizedOutputDayDateNode = "date";
        public const string LocalizedOutputDaySignNumberNode = "signnumber";
        public const string LocalizedOutputDaySignNameNode = "signname";

        /*
         * Константы для элементов класса LocalizedOutputWorship
        */
        public const string LocalizedOutputWorshipNodeName = "worship";
        public const string LocalizedOutputWorshipIdAttrName = "id";
        public const string LocalizedOutputWorshipTimeAttrName = "time";
        public const string LocalizedOutputWorshipNameNode = "name";
        public const string LocalizedOutputWorshipAdditionalNameAttrName = "additionalname";
        public const string LocalizedOutputWorshipChildNodeName = "items";

        /*
         * Константы для элементов класса LocalizedOutputSection
        */
        public const string LocalizedOutputSectionNodeName = "item";
        public const string LocalizedOutputSectionKindTextNodeName = "kindtext";
        public const string LocalizedOutputSectionKindAttrName = "kind";
        public const string LocalizedOutputSectionChildNodeName = "paragraphs";

        /*
         * Константы для элементов класса ParagraphViewModel 
        */
        public const string ParagraphNodeName = "p";
        public const string ParagraphLanguageNodeName = "language";
        public const string ParagraphTextNodeName = "text";
        public const string ParagraphStyleNodeName = "style";
        public const string ParagraphNoteNodeName = "note";
    }
}
