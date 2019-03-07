using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Psalter
{
    public class Kathisma : EntityBase<int>, IPsalterElement
    {
        public virtual TypiconVersion TypiconVersion { get; set; }
        public int Number { get; set; }
        public virtual ItemText NumberName { get; set; }
        public virtual List<SlavaElement> SlavaElements { get; set; } = new List<SlavaElement>();

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
