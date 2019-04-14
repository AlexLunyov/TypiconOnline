﻿using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Psalter
{
    public class Kathisma : ValueObjectBase<ITypiconSerializer>, IHasId<int>, IPsalterElement
    {
        public int Id { get; set; }
        public virtual TypiconVersion TypiconVersion { get; set; }
        public int Number { get; set; }
        public virtual ItemText NumberName { get; set; }
        public virtual List<SlavaElement> SlavaElements { get; set; } = new List<SlavaElement>();

        protected override void Validate(ITypiconSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
