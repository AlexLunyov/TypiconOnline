using TypiconOnline.AppServices.Common;

namespace TypiconOnline.AppServices.Migration.Psalter
{
    public class PsalterCsReader : PsalterRuReader
    {
        public PsalterCsReader(string folderPath, string language) : base(folderPath, language)
        {
            KATHISMA_TEXT = "Каfjсма";
            PSALM_TEXT = "[Pал0мъ]";
            SLAVA_TEXT = "Слaва:";
        }

        protected override bool TryParse(string str, out int i)
        {
            str = str.Replace(".", string.Empty);
            return IntCs.TryParse(str, out i);
        }
    }
}
