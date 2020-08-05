using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Versioned.Typicon
{
    public abstract class VersionLinkedBase<T, U> : VersionPrevBase<U> where T: EntityBase, IHasId<int>, new()
                                                                where U: VersionBase, new()
    {
        public int VersionOwnerId { get; set; }
        public virtual T VersionOwner { get; set; }
    }
}
