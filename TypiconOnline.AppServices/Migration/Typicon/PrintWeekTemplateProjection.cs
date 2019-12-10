using System;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    [Serializable]
    public class PrintWeekTemplateProjection
    {
        public int DaysPerPage { get; set; }
        public byte[] PrintFile { get; set; }
        public string PrintFileName { get; set; }
    }
}