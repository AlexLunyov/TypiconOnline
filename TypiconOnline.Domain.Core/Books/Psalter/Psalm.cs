using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Core.Books.Psalter
{
    public class Psalm : BookElementBase<BookReading>, IPsalterElement, IAggregateRoot
    {
        private BookReading reading;
        public Psalm() { }

        public virtual int Number { get; set; }

        public override BookReading GetElement()
        {
            if (reading == null)
            {
                reading = base.GetElement() ?? new BookReading();
            }

            return reading;
        }
    }
}
