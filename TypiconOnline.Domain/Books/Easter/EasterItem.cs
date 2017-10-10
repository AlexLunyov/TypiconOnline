using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books.Easter
{
    public class EasterItem : IAggregateRoot
    {
        public EasterItem() { }
        public DateTime Date { get; set; }
    }
}
