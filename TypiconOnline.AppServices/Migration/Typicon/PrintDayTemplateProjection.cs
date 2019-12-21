using System;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    [Serializable]
    public class PrintDayTemplateProjection
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int? Icon { get; set; }
        public byte[] PrintFile { get; set; }
        public string PrintFileName { get; set; }
    }
}