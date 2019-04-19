using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Psalter
{
    public class Kathisma : ValueObjectBase<ITypiconSerializer>, ITypiconVersionChild, IPsalterElement
    {
        public int Id { get; set; }
        /// <summary>
        /// Id Устава (TypiconVersion)
        /// </summary>
        public virtual int TypiconVersionId { get; set; }
        public virtual TypiconVersion TypiconVersion { get; set; }
        public int Number { get; set; }
        public virtual ItemText NumberName { get; set; }
        public virtual List<SlavaElement> SlavaElements { get; set; } = new List<SlavaElement>();

        protected override void Validate(ITypiconSerializer serializer)
        {
            if (SlavaElements.Count == 0 || SlavaElements.Count > 3)
            {
                AddBrokenConstraint(new BusinessConstraint("Количество дочерних элементов может быть от 1 до 3", nameof(Kathisma)));
            }
        }
    }
}
