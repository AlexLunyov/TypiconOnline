namespace TypiconOnline.Domain.Typicon
{
    public class TriodionRule : DayRule
    {
        public virtual int DaysFromEaster { get; set; }
        /// <summary>
        /// Означает, что правило будет добавляться в качестве дополнения к Правилу Минеи 
        /// при формировании последовательностей богослужений.
        /// Используется при формировании настроек для обработчика Правил
        /// </summary>
        public bool IsTransparent { get; set; }
    }
}
