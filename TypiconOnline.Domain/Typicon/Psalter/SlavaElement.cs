using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Psalter
{
    public class SlavaElement : ValueObjectBase<ITypiconSerializer>, IHasId<int>, IPsalterElement
    {
        public int Id { get; set; }
        //нужен ли???
        public int Index { get; }
        public virtual List<PsalmLink> PsalmLinks { get; set; } = new List<PsalmLink>();

        protected override void Validate(ITypiconSerializer serializer)
        {
            if (PsalmLinks.Count == 0)
            {
                AddBrokenConstraint(new BusinessConstraint("Слава должна иметь ссылки на Псалмы."), nameof(SlavaElement));
            }
            else
            {
                foreach (var pl in PsalmLinks)
                {
                    if (!pl.IsValid(serializer))
                    {
                        //добавляем ломаные правила к родителю
                        AppendAllBrokenConstraints(pl, serializer);
                    }
                }
            }
        }
    }
}
