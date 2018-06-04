using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Core.Books.Easter
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
