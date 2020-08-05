using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Versioned.Typicon
{
    public abstract class VersionBase : IHasId<int>
    {
        public VersionBase()
        {
            CreateDate = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
