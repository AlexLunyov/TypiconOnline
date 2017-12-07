using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books.Psalter
{
    public class Psalm : BookElementBase<BookReading>, IPsalterElement
    {
        public Psalm() { }

        public virtual int Number { get; set; }
        //public virtual List<PsalmStihos> Annotation { get; set; }
        //public virtual List<PsalmStihos> Text { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
