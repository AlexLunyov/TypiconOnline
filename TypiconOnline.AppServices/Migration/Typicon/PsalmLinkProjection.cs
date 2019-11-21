namespace TypiconOnline.AppServices.Migration.Typicon
{
    public class PsalmLinkProjection
    {
        public PsalmLinkProjection() { }
        public int PsalmId { get; set; }
        public int? StartStihos { get; set; }
        public int? EndStihos { get; set; }
    }
}