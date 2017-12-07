using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Psalter
{
    public class Kathisma : EntityBase<int>, IPsalterElement
    {
        public TypiconEntity TypiconEntity { get; set; }
        public int Number { get; set; }
        public ItemText NumberName { get; set; }
        public List<SlavaElement> SlavaElements { get; set; } = new List<SlavaElement>();

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
