using System;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Psalter
{
    public class PsalmLink : ValueObjectBase<ITypiconSerializer>, IHasId<int>, IPsalterElement
    {
        public int Id { get; set; }
        public int PsalmId { get; set; }
        public virtual Psalm Psalm { get; set; }
        public int? StartStihos { get; set; }
        public int? EndStihos { get; set; }

        protected override void Validate(ITypiconSerializer serializer)
        {
            if (StartStihos.HasValue 
                && EndStihos.HasValue
                && StartStihos > EndStihos)
            {
                AddBrokenConstraint(new BusinessConstraint("Начальный стих не может иметь значения больше, чем конечный стих."), nameof(PsalmLink));
            }

            if (!Psalm.IsValid(serializer))
            {
                AppendAllBrokenConstraints(Psalm, serializer, nameof(PsalmLink));
            }
        }
    }
}
