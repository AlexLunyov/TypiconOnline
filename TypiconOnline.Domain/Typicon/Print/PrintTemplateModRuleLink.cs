namespace TypiconOnline.Domain.Typicon.Print
{
    public class PrintTemplateModRuleLink<T> where T : ModRuleEntity, new()
    {
        public int TemplateId { get; set; }
        public virtual PrintDayTemplate Template { get; set; }

        public int EntityId { get; set; }

        public virtual T Entity { get; set; }
    }
}