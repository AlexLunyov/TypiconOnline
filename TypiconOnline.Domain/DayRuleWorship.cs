using System;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain
{
    /// <summary>
    /// Класс соединения правил и дневных служб
    /// </summary>
    public class DayRuleWorship: IComparable 
    {
        public int DayRuleId { get; set; }
        public virtual DayRule DayRule { get; set; }
        public int DayWorshipId { get; set; }
        public virtual DayWorship DayWorship { get; set; }
        /// <summary>
        /// Порядок в коллекции, 1-ориентированный.
        /// Необходим для сортировки в коллекциях DayWorship
        /// </summary>
        public int Order { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is DayRuleWorship worship)
            {
                return Order.CompareTo(worship.Order);
            }
            else
            {
                throw new Exception("Невозможно сравнить два объекта");
            }
        }
    }
}