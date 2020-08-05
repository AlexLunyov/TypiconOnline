using System;
using System.Collections.Generic;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Versioned.Typicon
{
    public class TypiconVersion : VersionPrevBase<TypiconVersion>
    {
        public int TypiconId { get; set; }
        public virtual Typicon Typicon { get; set; }

        public virtual List<SignVersion> Signs { get; set; } = new List<SignVersion>();
    }
}
