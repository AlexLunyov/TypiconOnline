using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Psalter
{
    public class SlavaElement : EntityBase<int>, IPsalterElement
    {
        //нужен ли???
        public int Index { get; }
        public virtual List<PsalmLink> PsalmLinks { get; set; } = new List<PsalmLink>();

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
