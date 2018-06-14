using System;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books.Easter
{
    /// <summary>
    /// Агрегат, хранящий дату Пасхи
    /// </summary>
    public class EasterItem : IAggregateRoot
    {
        public EasterItem() { }
        public DateTime Date { get; set; }

        public override int GetHashCode()
        {
            return Date.GetHashCode();
        }
    }
}
