using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.Versioned.Typicon
{
    public abstract class VersionPrevBase<U> : VersionBase where U : VersionBase, new()
    {
        public int? PreviousVersionId { get; set; }
        public virtual U PreviousVersion { get; set; }
    }
}
