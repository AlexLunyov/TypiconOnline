using System;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Psalter
{
    public class PsalmLink : ValueObjectBase<ITypiconSerializer>, IHasId<int>, IPsalterElement
    {
        public int Id { get; set; }
        public virtual Psalm Psalm { get; set; }
        public int? StartStihos { get; set; }
        public int? EndStihos { get; set; }

        protected override void Validate(ITypiconSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
