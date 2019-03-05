using System;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Psalter
{
    public class PsalmLink : EntityBase<int>, IPsalterElement
    {
        public virtual Psalm Psalm { get; set; }
        public int? StartStihos { get; set; }
        public int? EndStihos { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
